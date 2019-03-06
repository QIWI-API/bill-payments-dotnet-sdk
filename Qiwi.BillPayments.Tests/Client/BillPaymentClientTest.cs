using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Utils;
using Qiwi.BillPayments.Web;

namespace Qiwi.BillPayments.Tests.Client
{
    public partial class BillPaymentClientTest
    {
        private static Uri PayUri => (Uri) _testContext.Properties["payUri"];

        private static TestContext _testContext;
        
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
            _testContext.Properties.Add(
                "payUri",
                new Uri("https://oplata.qiwi.com/form/?invoice_uid=bb773791-9bd9-42c1-b8fc-3358cd108422")
            );
            ClassInitialize_Api();
            ClassInitialize_Fake();
        }
        
        [TestMethod]
        [DataRow("http://test.ru/")]
        public void TestAppendSuccessUrl(string successUrl)
        {
            // Prepare
            var baseBillResponse = new BillResponse
            {
                PayUrl = PayUri
            };
            // Test
            var billResponse = BillPaymentsClient.AppendSuccessUrl(baseBillResponse, new Uri(successUrl));
            var query = HttpUtility.ParseQueryString(billResponse.PayUrl.Query);
            // Assert
            Assert.AreEqual(successUrl, query["successUrl"]);
        }
        
        private static void PrepareBill(
            out CreateBillInfo createBillInfo,
            string value = null,
            string currency = null,
            string comment = null,
            string email = null,
            string account = null,
            string phone = null,
            string successUrl = null
        )
        {
            createBillInfo = new CreateBillInfo
            {
                BillId = Guid.NewGuid().ToString(),
                Amount = new MoneyAmount
                {
                    ValueString = value,
                    CurrencyString = currency
                },
                Comment = comment,
                ExpirationDateTime = BillPaymentsUtils.GetTimeoutDate(),
                Customer = new Customer
                {
                    Email = email,
                    Account = account,
                    Phone = phone
                },
                SuccessUrl = string.IsNullOrEmpty(successUrl) ? null : new Uri(successUrl)
            };
        }
        
        private static void PrepareRefund(
            out string refundId,
            out MoneyAmount moneyAmount,
            string amount = null,
            string currency = null
        )
        {
            refundId = Guid.NewGuid().ToString();
            moneyAmount = new MoneyAmount
            {
                ValueString = amount,
                CurrencyString = currency
            };
        }
        
        private static void TestCreateBill_Assert(CreateBillInfo createBillInfo, IFingerprint fingerprint, BillResponse billResponse)
        {
            var query = HttpUtility.ParseQueryString(billResponse.PayUrl.Query);
            Assert.AreEqual(createBillInfo.BillId, billResponse.BillId, "Same bill id");
            Assert.AreEqual(createBillInfo.Amount.ValueDecimal, billResponse.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(createBillInfo.Amount.CurrencyEnum, billResponse.Amount.CurrencyEnum, "Same amount currency");
            Assert.AreEqual(createBillInfo.Comment, billResponse.Comment, "Same comment");
            Assert.AreEqual(createBillInfo.ExpirationDateTime, billResponse.ExpirationDateTime, "Same expiration dateTime");
            Assert.AreEqual(createBillInfo.Customer.Email, billResponse.Customer.Email, "Same email");
            Assert.AreEqual(createBillInfo.Customer.Account, billResponse.Customer.Account, "Same account");
            Assert.AreEqual(createBillInfo.Customer.Phone, billResponse.Customer.Phone, "Same phone");
            Assert.AreEqual(createBillInfo.SuccessUrl, query["successUrl"], "Same success url");
            Assert.AreEqual(BillStatusEnum.Waiting, billResponse.Status.ValueEnum, "Status value preset");
            Assert.AreEqual(fingerprint.GetClientName(), billResponse.CustomFields.ApiClient, "Api client preset");
            Assert.AreEqual(fingerprint.GetClientVersion(), billResponse.CustomFields.ApiClientVersion, "Api client preset");
        }
        
        private static void TestGetBillInfo_Assert(BillResponse bill, BillResponse billResponse)
        {
            Assert.AreEqual(bill.BillId, billResponse.BillId, "Same bill id");
            Assert.AreEqual(bill.Amount.ValueDecimal, billResponse.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(bill.Amount.CurrencyEnum, billResponse.Amount.CurrencyEnum, "Same amount currency");
            Assert.AreEqual(bill.Comment, billResponse.Comment, "Same comment");
            Assert.AreEqual(bill.ExpirationDateTime, billResponse.ExpirationDateTime, "Same expiration dateTime");
            Assert.AreEqual(bill.Customer.Email, billResponse.Customer.Email, "Same email");
            Assert.AreEqual(bill.Customer.Account, billResponse.Customer.Account, "Same account");
            Assert.AreEqual(bill.Customer.Phone, billResponse.Customer.Phone, "Same phone");
            Assert.AreEqual(bill.SiteId, billResponse.SiteId, "Same site id");
            Assert.AreEqual(bill.CreationDateTime, billResponse.CreationDateTime, "Same creation dateTime");
            Assert.AreEqual(bill.Status.ChangedDateTime, billResponse.Status.ChangedDateTime, "Same status changed dateTime");
            Assert.AreEqual(bill.Status.ValueEnum, billResponse.Status.ValueEnum, "Same status value");
            Assert.AreEqual(bill.CustomFields.ApiClient, billResponse.CustomFields.ApiClient, "Api client preset");
            Assert.AreEqual(bill.CustomFields.ApiClientVersion, billResponse.CustomFields.ApiClientVersion, "Api client version preset");
        }
        
        private static void TestCancelBill_Assert(BillResponse bill, BillResponse billResponse)
        {
            Assert.AreEqual(bill.BillId, billResponse.BillId, "Same bill id");
            Assert.AreEqual(bill.Amount.ValueDecimal, billResponse.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(bill.Amount.CurrencyEnum, billResponse.Amount.CurrencyEnum, "Same amount currency");
            Assert.AreEqual(bill.Comment, billResponse.Comment, "Same comment");
            Assert.AreEqual(bill.ExpirationDateTime, billResponse.ExpirationDateTime, "Same expiration dateTime");
            Assert.AreEqual(bill.Customer.Email, billResponse.Customer.Email, "Same email");
            Assert.AreEqual(bill.Customer.Account, billResponse.Customer.Account, "Same account");
            Assert.AreEqual(bill.Customer.Phone, billResponse.Customer.Phone, "Same phone");
            Assert.AreEqual(bill.SiteId, billResponse.SiteId, "Same site id");
            Assert.AreEqual(bill.CreationDateTime, billResponse.CreationDateTime, "Same creation dateTime");
            Assert.AreNotEqual(bill.Status.ValueEnum, billResponse.Status.ValueEnum, "Same status value");
            Assert.AreEqual(bill.CustomFields.ApiClient, billResponse.CustomFields.ApiClient, "Api client preset");
            Assert.AreEqual(bill.CustomFields.ApiClientVersion, billResponse.CustomFields.ApiClientVersion, "Api client version preset");
            Assert.AreEqual(BillStatusEnum.Rejected, billResponse.Status.ValueEnum, "Status value preset");
        }
        
        private static void TestRefundBill_Assert(string refundId, MoneyAmount moneyAmount, RefundResponse refundResponse)
        {
            Assert.AreEqual(refundId, refundResponse.RefundId, "Same refund id");
            Assert.AreEqual(moneyAmount.ValueDecimal, refundResponse.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(moneyAmount.CurrencyEnum, refundResponse.Amount.CurrencyEnum, "Same currency value");
            Assert.AreEqual(RefundStatusEnum.Partial, refundResponse.StatusEnum, "Status preset");
        }
        
        private static void TestGetRefundInfo_Assert(RefundResponse refund, RefundResponse refundResponse)
        {
            Assert.AreEqual(refund.RefundId, refundResponse.RefundId, "Same refund id");
            Assert.AreEqual(refund.Amount.ValueDecimal, refundResponse.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(refund.Amount.CurrencyEnum, refundResponse.Amount.CurrencyEnum, "Same currency value");
            Assert.AreEqual(refund.StatusEnum, refundResponse.StatusEnum, "Same status");
        }
    }
}
