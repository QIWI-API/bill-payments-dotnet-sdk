using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Json.Newtonsoft;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Tests.Web;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Tests.Client
{
    public partial class BillPaymentClientTest
    {
        private class FakeApiPresets : ApiPresets
        {
            public FakeClient Client { get; set; }
        }
        
        private static IReadOnlyDictionary<BillPaymentsClient, FakeApiPresets> _fakeDataRows;
        
        private static void ClassInitialize_Fake()
        {
            _fakeDataRows = new Dictionary<BillPaymentsClient, FakeApiPresets>(
            new[]
                {
                    new FakeClient(ObjectMapperFactory.create()),
                    new FakeClient(ObjectMapperFactory.create<NewtonsoftMapper>())
                }
                .Select(fakeClient => new KeyValuePair<BillPaymentsClient, FakeApiPresets>(
                    BillPaymentsClientFactory.create(Config.MerchantSecretKey, fakeClient, fakeClient.ObjectMapper),
                    new FakeApiPresets
                    {
                        Client = fakeClient,
                        Bills = new List<BillResponse>(),
                        Refunds = new List<RefundResponse>()
                    }
                ))
            );
        }
        
        [Test]
        [Order(1)]
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
                var billResponse = new BillResponse
                {
                    BillId = createBillInfo.BillId,
                    Amount = createBillInfo.Amount,
                    Comment = createBillInfo.Comment,
                    ExpirationDateTime = createBillInfo.ExpirationDateTime,
                    Customer = createBillInfo.Customer,
                    PayUrl = _payUri,
                    Status = new ResponseStatus
                    {
                        ValueEnum = BillStatusEnum.Waiting,
                        ChangedDateTime = DateTime.Now
                    },
                    CustomFields = new CustomFields
                    {
                        ApiClient = fingerprint.getClientName(),
                        ApiClientVersion = fingerprint.getClientVersion()
                    }
                };
                ResetClient(presets.Client, billResponse);
                // Test
                presets.Client.OnRequest += TestCreateBill_OnRequest;
                billResponse = client.createBill(createBillInfo);
                presets.Client.OnRequest -= TestCreateBill_OnRequest;
                presets.Bills.Add(billResponse);
                // Assert
                TestCreateBill_Assert(createBillInfo, fingerprint, billResponse);
            }
        }
        
        [Test]
        [Order(2)]
        public void TestGetBillInfo_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                foreach (var bill in presets.Bills)
                {
                    
                    ResetClient(presets.Client, bill);
                    // Test
                    presets.Client.OnRequest += TestGetBillInfo_OnRequest;
                    var billResponse = client.getBillInfo(bill.BillId);
                    presets.Client.OnRequest -= TestGetBillInfo_OnRequest;
                    // Assert
                    TestGetBillInfo_Assert(bill, billResponse);
                }
            }
        }
        
        [Test]
        [Order(3)]
        public void TestCancelBill_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                foreach (var bill in presets.Bills)
                {
                    var fingerprint = client.getFingerprint();
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
                            ApiClient = fingerprint.getClientName(),
                            ApiClientVersion = fingerprint.getClientVersion()
                        }
                    };
                    ResetClient(presets.Client, rejectedBill);
                    // Test
                    presets.Client.OnRequest += TestCancelBill_OnRequest;
                    var billResponse = client.cancelBill(bill.BillId);
                    presets.Client.OnRequest -= TestCancelBill_OnRequest;
                    // Assert
                    TestCancelBill_Assert(bill, billResponse);
                }
            }
        }
        
        [Test]
        [Order(1)]
        public void TestRefundBill_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                PrepareRefund(
                    "0.01",
                    "RUB",
                    out var refundId,
                    out var moneyAmount
                );
                var refundResponse = new RefundResponse
                {
                    Amount = moneyAmount,
                    Datetime = DateTime.Now,
                    RefundId = refundId,
                    StatusEnum = RefundStatusEnum.Partial
                };
                ResetClient(presets.Client, refundResponse);
                // Test
                presets.Client.OnRequest += TestRefundBill_OnRequest;
                refundResponse = client.refundBill(Config.BillIdForRefundTest, refundId, moneyAmount);
                presets.Client.OnRequest -= TestRefundBill_OnRequest;
                presets.Refunds.Add(refundResponse);
                // Assert
                TestRefundBill_Assert(refundId, moneyAmount, refundResponse);
            }
        }
        
        [Test]
        [Order(2)]
        public void TestGetRefundInfo_Fake()
        {
            // Prepare
            foreach (var (client, presets) in _fakeDataRows)
            {
                foreach (var refund in presets.Refunds)
                {
                    // Test
                    presets.Client.OnRequest += TestGetRefundInfo_OnRequest;
                    var refundResponse = client.getRefundInfo(Config.BillIdForRefundTest, refund.RefundId);
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
                Body = client.ObjectMapper.writeValue(body),
                HttpStatus = HttpStatusCode.OK
            };
        }

        private static void TestCreateBill_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var billResponse = sender.ObjectMapper.readValue<BillResponse>(args.ResponseData.Body);
            var createBillRequest = sender.ObjectMapper.readValue<CreateBillRequest>(args.entityOpt);
            Assert.AreEqual(1, args.counter, "One request");
            Assert.AreEqual("PUT", args.method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + billResponse.BillId, args.url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.headers["Accept"], "Accept bearer");
            Assert.AreEqual("application/json", args.headers["Content-Type"], "Content type bearer");
            Assert.AreEqual(billResponse.Amount.ValueDecimal, createBillRequest.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(billResponse.Amount.CurrencyEnum, createBillRequest.Amount.CurrencyEnum, "Same amount currency");
            Assert.AreEqual(billResponse.Comment, createBillRequest.Comment, "Same amount currency");
            Assert.AreEqual(billResponse.ExpirationDateTime, createBillRequest.ExpirationDateTime, "Same expiration datetime");
            Assert.AreEqual(billResponse.Customer.Email, createBillRequest.Customer.Email, "Same email");
            Assert.AreEqual(billResponse.Customer.Account, createBillRequest.Customer.Account, "Same account");
            Assert.AreEqual(billResponse.Customer.Phone, createBillRequest.Customer.Phone, "Same phone");
        }
        
        private static void TestGetBillInfo_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var billResponse = sender.ObjectMapper.readValue<BillResponse>(args.ResponseData.Body);
            Assert.AreEqual(1, args.counter, "One request");
            Assert.AreEqual("GET", args.method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + billResponse.BillId, args.url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.headers["Accept"], "Accept bearer");
            Assert.IsNull(args.entityOpt, "No body request");
        }

        private static void TestCancelBill_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var billResponse = sender.ObjectMapper.readValue<BillResponse>(args.ResponseData.Body);
            Assert.AreEqual(1, args.counter, "One request");
            Assert.AreEqual("POST", args.method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + billResponse.BillId + "/reject", args.url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.headers["Accept"], "Accept bearer");
            Assert.IsNull(args.entityOpt, "No body request");
        }
        
        private static void TestRefundBill_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var refundResponse = sender.ObjectMapper.readValue<RefundResponse>(args.ResponseData.Body);
            var refundBillRequest = sender.ObjectMapper.readValue<RefundBillRequest>(args.entityOpt);
            Assert.AreEqual(1, args.counter, "One request");
            Assert.AreEqual("PUT", args.method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + Config.BillIdForRefundTest + "/refunds/" + refundResponse.RefundId, args.url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.headers["Accept"], "Accept bearer");
            Assert.AreEqual("application/json", args.headers["Content-Type"], "Content type bearer");
            Assert.AreEqual(refundResponse.Amount.ValueDecimal, refundBillRequest.Amount.ValueDecimal, "Same amount value");
            Assert.AreEqual(refundResponse.Amount.CurrencyEnum, refundBillRequest.Amount.CurrencyEnum, "Same amount currency");
        }
        
        private static void TestGetRefundInfo_OnRequest(FakeClient sender, FakeClientRequestEventArgs args)
        {
            var refundResponse = sender.ObjectMapper.readValue<RefundResponse>(args.ResponseData.Body);
            Assert.AreEqual(1, args.counter, "One request");
            Assert.AreEqual("GET", args.method, "Equal method");
            Assert.AreEqual(BillPaymentsClient.BillsUrl + Config.BillIdForRefundTest + "/refunds/" + refundResponse.RefundId, args.url, "Equal url");
            Assert.AreEqual("Bearer " + Config.MerchantSecretKey, args.headers["Authorization"], "Authorization bearer");
            Assert.AreEqual("application/json", args.headers["Accept"], "Accept bearer");
            Assert.IsNull(args.entityOpt, "No body request");
        }
    }
}
