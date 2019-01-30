using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Json.Newtonsoft;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;

namespace Qiwi.BillPayments.Tests.Client
{
    [TestClass]
    public partial class BillPaymentClientTest
    {
        private class ApiPresets
        {
            public List<BillResponse> Bills { get; set; }
            public List<RefundResponse> Refunds { get; set; }
        }
        
        private static IReadOnlyDictionary<BillPaymentsClient, ApiPresets> ApiDataRows
            => (Dictionary<BillPaymentsClient, ApiPresets>) _testContext.Properties["apiDataRows"];
        
        private static void ClassInitialize_Api()
        {
            _testContext.Properties.Add(
                "apiDataRows",
                new Dictionary<BillPaymentsClient, ApiPresets>
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
                }
            );
        }
        
        [TestMethod]
        [DataRow("200.345", "http://test.ru/")]
        public void TestCreatePaymentForm(string value, string successUrl)
        {
            // Prepare
            foreach (var (client, _) in ApiDataRows)
            {
                var paymentInfo = new PaymentInfo
                {
                    PublicKey = Config.MerchantPublicKey,
                    Amount = new MoneyAmount
                    {
                        ValueString = value
                    },
                    BillId = Guid.NewGuid().ToString(),
                    SuccessUrl = new Uri(successUrl)
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
        
        [TestMethod]
        [Priority(1)]
        [DataRow("200.345", "RUB", "test", "test@test.ru", "user uid on your side", "79999999999", "http://test.ru/")]
        public void TestCreateBill_Api(
            string value,
            string currency,
            string comment,
            string email,
            string account,
            string phone,
            string successUrl
        )
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
            {
                var fingerprint = client.getFingerprint();
                PrepareBill(
                    value,
                    currency,
                    comment,
                    email,
                    account,
                    phone,
                    successUrl,
                    out var createBillInfo
                );
                // Test
                var billResponse = client.createBill(createBillInfo);
                presets.Bills.Add(billResponse);
                // Assert
                TestCreateBill_Assert(createBillInfo, fingerprint, billResponse);
            }
        }
        
        [TestMethod]
        [Priority(2)]
        public void TestGetBillInfo_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
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
        
        [TestMethod]
        [Priority(3)]
        public void TestCancelBill_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
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
        
        [TestMethod]
        [Priority(1)]
        [DataRow("0.01", "RUB")]
        public void TestRefundBill_Api(string amount, string currency)
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
            {
                PrepareRefund(
                    amount,
                    currency,
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
        
        [TestMethod]
        [Priority(2)]
        public void TestGetRefundInfo_Api()
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
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
