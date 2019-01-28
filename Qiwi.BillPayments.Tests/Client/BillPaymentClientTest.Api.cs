using System;
using System.Collections.Generic;
using System.Web;
using NUnit.Framework;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Json.Newtonsoft;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;

namespace Qiwi.BillPayments.Tests.Client
{
    public partial class BillPaymentClientTest
    {
        private class ApiPresets
        {
            public List<BillResponse> Bills { get; set; }
            public List<RefundResponse> Refunds { get; set; }
        }
        
        private static IReadOnlyDictionary<BillPaymentsClient, ApiPresets> _apiDataRows;
        
        private static void ClassInitialize_Api()
        {
            _apiDataRows = new Dictionary<BillPaymentsClient, ApiPresets>
            {
                {
                    BillPaymentsClientFactory.create(Config.MerchantSecretKey),
                    new ApiPresets
                    {
                        Bills = new List<BillResponse>(),
                        Refunds = new List<RefundResponse>()
                    }
                },
                {
                    BillPaymentsClientFactory.create(
                        Config.MerchantSecretKey,
                        null,
                        ObjectMapperFactory.create<NewtonsoftMapper>()
                    ),
                    new ApiPresets
                    {
                        Bills = new List<BillResponse>(),
                        Refunds = new List<RefundResponse>()
                    }
                }
            };
        }
        
        [Test]
        public void TestCreatePaymentForm(string value, string successUrl)
        {
            // Prepare
            foreach (var (client, _) in _apiDataRows)
            {
                var paymentInfo = new PaymentInfo
                {
                    PublicKey = Config.MerchantPublicKey,
                    Amount = new MoneyAmount
                    {
                        ValueString = "200.345"
                    },
                    BillId = Guid.NewGuid().ToString(),
                    SuccessUrl = new Uri("http://test.ru/")
                };
                // Test
                var uri = client.createPaymentForm(paymentInfo);
                var query = HttpUtility.ParseQueryString(uri.Query);
                // Assert
                Assert.AreEqual("https", uri.Scheme, "Get create form correct scheme");
                Assert.AreEqual("oplata.qiwi.com", uri.Host, "Get create form correct host");
                Assert.AreEqual(443, uri.Port, "Get create form correct port");
                Assert.AreEqual("/create", uri.AbsolutePath, "Get create form correct path");
                Assert.AreEqual(Config.MerchantPublicKey, query["publicKey"], "Set publicKey parameter");
                Assert.AreEqual(paymentInfo.Amount.ValueString, query["amount"], "Set amount parameter");
                Assert.AreEqual(paymentInfo.BillId, query["billId"], "Set billId parameter");
                Assert.AreEqual(paymentInfo.SuccessUrl, query["successUrl"], "Set billId parameter");
            }
        }
        
        [Test]
        [Order(1)]
        public void TestCreateBill_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in _apiDataRows)
            {
                var fingerprint = client.getFingerprint();
                PrepareBill(
                    "200.345",
                    "RUB",
                    "test",
                    "test@test.ru",
                    "user uid on your side",
                    "79999999999",
                    "http://test.ru/",
                    out var createBillInfo
                );
                // Test
                var billResponse = client.createBill(createBillInfo);
                presets.Bills.Add(billResponse);
                // Assert
                TestCreateBill_Assert(createBillInfo, fingerprint, billResponse);
            }
        }
        
        [Test]
        [Order(2)]
        public void TestGetBillInfo_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in _apiDataRows)
            {
                Assert.AreNotSame(0, presets.Bills.Count, "Bill will be create");
                foreach (var bill in presets.Bills)
                {
                    // Test
                    var billResponse = client.getBillInfo(bill.BillId);
                    // Assert
                    TestGetBillInfo_Assert(bill, billResponse);
                }
            }
        }
        
        [Test]
        [Order(3)]
        public void TestCancelBill_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in _apiDataRows)
            {
                Assert.AreNotSame(0, presets.Bills.Count, "Bill will be create");
                foreach (var bill in presets.Bills)
                {
                    // Test
                    var billResponse = client.cancelBill(bill.BillId);
                    // Assert
                    TestCancelBill_Assert(bill, billResponse);
                }
            }
        }
        
        [Test]
        [Order(1)]
        public void TestRefundBill_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in _apiDataRows)
            {
                PrepareRefund(
                    "0.01",
                    "RUB",
                    out var refundId,
                    out var moneyAmount
                );
                // Test
                var refundResponse = client.refundBill(Config.BillIdForRefundTest, refundId, moneyAmount);
                presets.Refunds.Add(refundResponse);
                // Assert
                TestRefundBill_Assert(refundId, moneyAmount, refundResponse);
            }
        }
        
        [Test]
        [Order(2)]
        public void TestGetRefundInfo_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in _apiDataRows)
            {
                Assert.AreNotSame(0, presets.Bills.Count, "Refund will be create");
                foreach (var refund in presets.Refunds)
                {
                    // Test
                    var refundResponse = client.getRefundInfo(Config.BillIdForRefundTest, refund.RefundId);
                    // Assert
                    TestGetRefundInfo_Assert(refund, refundResponse);
                }
            }
        }
    }
}
