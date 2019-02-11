using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Json.Newtonsoft;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Tests.Web;

namespace Qiwi.BillPayments.Tests.Client
{
    public partial class BillPaymentClientTest
    {
        private class FakeApiPresets : ApiPresets
        {
            public FakeClient Client { get; set; }
        }
        
        private readonly IReadOnlyDictionary<BillPaymentsClient, FakeApiPresets> _fakeDataRows
            = (IReadOnlyDictionary<BillPaymentsClient, FakeApiPresets>) _testContext.Properties["fakeDataRows"];
        
        private static void ClassInitialize_Fake()
        {
            _testContext.Properties.Add(
                "fakeDataRows",
                new Dictionary<BillPaymentsClient, FakeApiPresets>(
                new[]
                    {
                        new FakeClient(ObjectMapperFactory.Create()),
                        new FakeClient(ObjectMapperFactory.Create<NewtonsoftMapper>())
                    }
                    .Select(fakeClient => new KeyValuePair<BillPaymentsClient, FakeApiPresets>(
                        BillPaymentsClientFactory.Create(Config.MerchantSecretKey, fakeClient, fakeClient.ObjectMapper),
                        new FakeApiPresets
                        {
                            Client = fakeClient,
                            Bills = new List<BillResponse>(),
                            Refunds = new List<RefundResponse>()
                        }
                    ))
                )
            );
        }
        
        [TestMethod]
        [Priority(1)]
        [DataRow("200.345", "RUB", "test", "test@test.ru", "user uid on your side", "79999999999", "http://test.ru/")]
        public void TestCreateBill_Fake(
            string value,
            string currency,
            string comment,
            string email,
            string account,
            string phone,
            string successUrl
        )
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                var fingerprint = client.GetFingerprint();
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
                var billResponse = new BillResponse
                {
                    BillId = createBillInfo.BillId,
                    Amount = createBillInfo.Amount,
                    Comment = createBillInfo.Comment,
                    ExpirationDateTime = createBillInfo.ExpirationDateTime,
                    Customer = createBillInfo.Customer,
                    PayUrl = PayUri,
                    Status = new ResponseStatus
                    {
                        ValueEnum = BillStatusEnum.Waiting,
                        ChangedDateTime = DateTime.Now
                    },
                    CustomFields = new CustomFields
                    {
                        ApiClient = fingerprint.GetClientName(),
                        ApiClientVersion = fingerprint.GetClientVersion()
                    }
                };
                ResetClient(presets.Client, billResponse);
                // Test
                presets.Client.OnRequest += TestCreateBill_OnRequest;
                billResponse = client.CreateBill(createBillInfo);
                presets.Client.OnRequest -= TestCreateBill_OnRequest;
                presets.Bills.Add(billResponse);
                // Assert
                TestCreateBill_Assert(createBillInfo, fingerprint, billResponse);
            }
        }
        
        [TestMethod]
        [Priority(2)]
        public void TestGetBillInfo_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                Assert.AreNotSame(0, presets.Bills.Count, "Bill will be create");
                foreach (var bill in presets.Bills)
                {
                    
                    ResetClient(presets.Client, bill);
                    // Test
                    presets.Client.OnRequest += TestGetBillInfo_OnRequest;
                    var billResponse = client.GetBillInfo(bill.BillId);
                    presets.Client.OnRequest -= TestGetBillInfo_OnRequest;
                    // Assert
                    TestGetBillInfo_Assert(bill, billResponse);
                }
            }
        }
        
        [TestMethod]
        [Priority(3)]
        public void TestCancelBill_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                Assert.AreNotSame(0, presets.Bills.Count, "Bill will be create");
                foreach (var bill in presets.Bills)
                {
                    var fingerprint = client.GetFingerprint();
                    var rejectedBill = new BillResponse
                    {
                        BillId = bill.BillId,
                        Amount = bill.Amount,
                        Comment = bill.Comment,
                        ExpirationDateTime = bill.ExpirationDateTime,
                        Customer = bill.Customer,
                        PayUrl = bill.PayUrl,
                        Status = new ResponseStatus
                        {
                            ValueEnum = BillStatusEnum.Rejected,
                            ChangedDateTime = bill.Status.ChangedDateTime
                        },
                        CustomFields = new CustomFields
                        {
                            ApiClient = fingerprint.GetClientName(),
                            ApiClientVersion = fingerprint.GetClientVersion()
                        }
                    };
                    ResetClient(presets.Client, rejectedBill);
                    // Test
                    presets.Client.OnRequest += TestCancelBill_OnRequest;
                    var billResponse = client.CancelBill(bill.BillId);
                    presets.Client.OnRequest -= TestCancelBill_OnRequest;
                    // Assert
                    TestCancelBill_Assert(bill, billResponse);
                }
            }
        }
        
        [TestMethod]
        [Priority(1)]
        [DataRow("0.01", "RUB")]
        public void TestRefundBill_Fake(string amount, string currency)
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                PrepareRefund(
                    amount,
                    currency,
                    out var refundId,
                    out var moneyAmount
                );
                var refundResponse = new RefundResponse
                {
                    Amount = moneyAmount,
                    DateTime = DateTime.Now,
                    RefundId = refundId,
                    StatusEnum = RefundStatusEnum.Partial
                };
                ResetClient(presets.Client, refundResponse);
                // Test
                presets.Client.OnRequest += TestRefundBill_OnRequest;
                refundResponse = client.RefundBill(Config.BillIdForRefundTest, refundId, moneyAmount);
                presets.Client.OnRequest -= TestRefundBill_OnRequest;
                presets.Refunds.Add(refundResponse);
                // Assert
                TestRefundBill_Assert(refundId, moneyAmount, refundResponse);
            }
        }
        
        [TestMethod]
        [Priority(2)]
        public void TestGetRefundInfo_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                Assert.AreNotSame(0, presets.Bills.Count, "Refund will be create");
                foreach (var refund in presets.Refunds)
                {
                    ResetClient(presets.Client, refund);
                    // Test
                    presets.Client.OnRequest += TestGetRefundInfo_OnRequest;
                    var refundResponse = client.GetRefundInfo(Config.BillIdForRefundTest, refund.RefundId);
                    presets.Client.OnRequest -= TestGetRefundInfo_OnRequest;
                    // Assert
                    TestGetRefundInfo_Assert(refund, refundResponse);
                }
            }
        }

        private static void ResetClient(FakeClient client, object body)
        {
            client.RequestCounter = 0;
            client.DefaultResponseData = new ResponseData
            {
                Body = client.ObjectMapper.WriteValue(body),
                HttpStatus = HttpStatusCode.OK
            };
        }

        private static void TestCreateBill_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var billResponse = sender.ObjectMapper.ReadValue<BillResponse>(args.ResponseData.Body);
            var createBillRequest = sender.ObjectMapper.ReadValue<CreateBillRequest>(args.EntityOpt);
            Assert.AreEqual(1, args.Counter, "One request");
            Assert.AreEqual("PUT", args.Method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + billResponse.BillId, args.Url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.Headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.Headers["Accept"], "Accept bearer");
            Assert.AreEqual("application/json", args.Headers["Content-Type"], "Content type bearer");
            Assert.AreEqual(billResponse.Amount.ValueDecimal, createBillRequest.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(billResponse.Amount.CurrencyEnum, createBillRequest.Amount.CurrencyEnum, "Same amount currency");
            Assert.AreEqual(billResponse.Comment, createBillRequest.Comment, "Same amount currency");
            Assert.AreEqual(billResponse.ExpirationDateTime, createBillRequest.ExpirationDateTime, "Same expiration dateTime");
            Assert.AreEqual(billResponse.Customer.Email, createBillRequest.Customer.Email, "Same email");
            Assert.AreEqual(billResponse.Customer.Account, createBillRequest.Customer.Account, "Same account");
            Assert.AreEqual(billResponse.Customer.Phone, createBillRequest.Customer.Phone, "Same phone");
        }
        
        private static void TestGetBillInfo_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var billResponse = sender.ObjectMapper.ReadValue<BillResponse>(args.ResponseData.Body);
            Assert.AreEqual(1, args.Counter, "One request");
            Assert.AreEqual("GET", args.Method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + billResponse.BillId, args.Url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.Headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.Headers["Accept"], "Accept bearer");
            Assert.IsNull(args.EntityOpt, "No body request");
        }

        private static void TestCancelBill_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var billResponse = sender.ObjectMapper.ReadValue<BillResponse>(args.ResponseData.Body);
            Assert.AreEqual(1, args.Counter, "One request");
            Assert.AreEqual("POST", args.Method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + billResponse.BillId + "/reject", args.Url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.Headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.Headers["Accept"], "Accept bearer");
            Assert.IsNull(args.EntityOpt, "No body request");
        }
        
        private static void TestRefundBill_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var refundResponse = sender.ObjectMapper.ReadValue<RefundResponse>(args.ResponseData.Body);
            var refundBillRequest = sender.ObjectMapper.ReadValue<RefundBillRequest>(args.EntityOpt);
            Assert.AreEqual(1, args.Counter, "One request");
            Assert.AreEqual("PUT", args.Method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + Config.BillIdForRefundTest + "/refunds/" + refundResponse.RefundId, args.Url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.Headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.Headers["Accept"], "Accept bearer");
            Assert.AreEqual("application/json", args.Headers["Content-Type"], "Content type bearer");
            Assert.AreEqual(refundResponse.Amount.ValueDecimal, refundBillRequest.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(refundResponse.Amount.CurrencyEnum, refundBillRequest.Amount.CurrencyEnum, "Same amount currency");
        }
        
        private static void TestGetRefundInfo_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var refundResponse = sender.ObjectMapper.ReadValue<RefundResponse>(args.ResponseData.Body);
            Assert.AreEqual(1, args.Counter, "One request");
            Assert.AreEqual("GET", args.Method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + Config.BillIdForRefundTest + "/refunds/" + refundResponse.RefundId, args.Url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.Headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.Headers["Accept"], "Accept bearer");
            Assert.IsNull(args.EntityOpt, "No body request");
        }
    }
}
