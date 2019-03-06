using System;
using System.Collections.Generic;
using System.Linq;
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
                        BillPaymentsClientFactory.Create(Config.MerchantSecretKey),
                        new ApiPresets
                        {
                            Bills = new List<BillResponse>(),
                            Refunds = new List<RefundResponse>()
                        }
                    },
                    {
                        BillPaymentsClientFactory.Create(
                            Config.MerchantSecretKey,
                            null,
                            ObjectMapperFactory.Create<NewtonsoftMapper>()
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
        [DataRow("200.345", null)]
        public void TestCreatePaymentForm(
            string value = null,
            string successUrl = null
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
                    SuccessUrl = string.IsNullOrEmpty(successUrl) ? null : new Uri(successUrl)
                };
                // Test
                var uri = client.CreatePaymentForm(paymentInfo);
                var query = HttpUtility.ParseQueryString(uri.Query);
                // Assert
                Assert.AreEqual("https", uri.Scheme, "Get create form correct scheme");
                Assert.AreEqual("oplata.qiwi.com", uri.Host, "Get create form correct host");
                Assert.AreEqual(443, uri.Port, "Get create form correct port");
                Assert.AreEqual("/create", uri.AbsolutePath, "Get create form correct path");
                Assert.AreEqual(Config.MerchantPublicKey, query["publicKey"], "Set publicKey parameter");
                Assert.AreEqual(paymentInfo.Amount.ValueString, query["amount"], "Set amount parameter");
                Assert.AreEqual(paymentInfo.BillId, query["billId"], "Set billId parameter");
                if (string.IsNullOrEmpty(successUrl))
                {
                    Assert.IsFalse(query.AllKeys.Contains("successUrl"), "Don't set successUrl parameter");
                }
                else
                {
                    Assert.AreEqual(paymentInfo.SuccessUrl, query["successUrl"], "Set successUrl parameter");
                }
            }
        }
        
        [TestMethod]
        [Priority(1)]
        [DataRow("200.345", "RUB", "test", "test@test.ru", "user uid on your side", "79999999999", "http://test.ru/")]
        public void TestCreateBill_Api(
            string value = null,
            string currency = null,
            string comment = null,
            string email = null,
            string account = null,
            string phone = null,
            string successUrl = null
        )
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
            {
                var fingerprint = client.GetFingerprint();
                PrepareBill(
                    out var createBillInfo,
                    value,
                    currency,
                    comment,
                    email,
                    account,
                    phone,
                    successUrl
                );
                // Test
                var billResponse = client.CreateBill(createBillInfo);
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
                    var billResponse = client.GetBillInfo(bill.BillId);
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
                    var billResponse = client.CancelBill(bill.BillId);
                    // Assert
                    TestCancelBill_Assert(bill, billResponse);
                }
            }
        }
        
        [TestMethod]
        [Priority(1)]
        [DataRow("0.01", "RUB")]
        public void TestRefundBill_Api(
            string amount = null,
            string currency = null
        )
        {
            Config.Required();
            // Prepare
            foreach (var (client, presets) in ApiDataRows)
            {
                PrepareRefund(
                    out var refundId,
                    out var moneyAmount,
                    amount,
                    currency
                );
                // Test
                var refundResponse = client.RefundBill(Config.BillIdForRefundTest, refundId, moneyAmount);
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
                    var refundResponse = client.GetRefundInfo(Config.BillIdForRefundTest, refund.RefundId);
                    // Assert
                    TestGetRefundInfo_Assert(refund, refundResponse);
                }
            }
        }
    }
}
