using System;
using System.Collections.Generic;
using System.Web;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Json.Newtonsoft;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;

namespace Qiwi.BillPayments.Tests.Client
{
    [TestFixture]
    public class BillPaymentClientApiTest : BillPaymentClientTestCase
    {
        private class ApiPresets
        {
            public List<BillResponse> Bills { get; set; }
            public List<RefundResponse> Refunds { get; set; }
        }
        
        private static readonly IReadOnlyDictionary<BillPaymentsClient, ApiPresets> ApiDataRows
            = new Dictionary<BillPaymentsClient, ApiPresets>
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
        
        [Test]
        [Sequential]
        public void TestCreatePaymentForm(
            [Values("200.345")] string value,
            [Values("http://test.ru/")] string successUrl
        )
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
        
        [Test]
        [Sequential]
        [Order(1)]
        public void TestCreateBill_Api(
            [Values("200.345")] string value,
            [Values("RUB")] string currency,
            [Values("test")] string comment,
            [Values("test@test.ru")] string email,
            [Values("user uid on your side")] string account,
            [Values("79999999999")] string phone,
            [Values("http://test.ru/")] string successUrl
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
        
        [Test]
        [Order(2)]
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
        
        [Test]
        [Order(3)]
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
        
        [Test]
        [Order(1)]
        [Sequential]
        public void TestRefundBill_Api(
            [Values("0.01")] string amount,
            [Values("RUB")] string currency
        )
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
        
        [Test]
        [Order(2)]
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
