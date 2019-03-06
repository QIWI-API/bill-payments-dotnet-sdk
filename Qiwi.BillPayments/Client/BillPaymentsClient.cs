using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;
using Qiwi.BillPayments.Exception;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Web;

namespace Qiwi.BillPayments.Client
{
    /// <summary>
    /// QIWI Universal Payment Protocol API client.
    /// https://developer.qiwi.com/en/bill-payments/
    /// </summary>
    [ComVisible(true)]
    public class BillPaymentsClient
    {
        /// <summary>
        /// The API base URL.
        /// </summary>
        public const string BillsUrl = "https://api.qiwi.com/partner/bill/v1/bills/";
        
        /// <summary>
        /// The API dateTime format.
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffzzz";
        
        /// <summary>
        /// The request mapping intercessor.
        /// </summary>
        private readonly RequestMappingIntercessor _requestMappingIntercessor;
        
        /// <summary>
        /// The HTTP headers dictionary.
        /// </summary>
        private readonly Dictionary<string, string> _headers;
        
        /// <summary>
        /// The API client fingerprint.
        /// </summary>
        private readonly IFingerprint _fingerprint;
        
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="secretKey">The merchant secret key.</param>
        /// <param name="requestMappingIntercessor">The API request mapper.</param>
        /// <param name="fingerprint">The client Fingerprint.</param>
        public BillPaymentsClient(
            string secretKey,
            RequestMappingIntercessor requestMappingIntercessor,
            IFingerprint fingerprint
        )
        {
            _headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Accept", "application/json"},
                {"Authorization", "Bearer " + secretKey}
            };
            _requestMappingIntercessor = requestMappingIntercessor;
            _fingerprint = fingerprint;
        }
        
        /// <summary>
        /// Append success URL parameter to payment URL in invoice.
        /// </summary>
        /// <param name="response">The invoice.</param>
        /// <param name="successUrl">The success URL.</param>
        /// <returns>New invoice witch updated payment URL.</returns>
        [ComVisible(true)]
        public static BillResponse AppendSuccessUrl(BillResponse response, Uri successUrl)
        {
            return AppendSuccessUrl<BillResponse>(response, successUrl);
        }
        
        /// <summary>
        /// Append success URL parameter to payment URL in invoice.
        /// </summary>
        /// <param name="response">The invoice.</param>
        /// <param name="successUrl">The success URL.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>New invoice witch updated payment URL.</returns>
        [ComVisible(true)]
        public static T AppendSuccessUrl<T>(BillResponse response, Uri successUrl) where T : BillResponse
        {
            var uriBuilder = new UriBuilder(response.PayUrl);
            var parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            parameters["successUrl"] = successUrl.ToString();
            uriBuilder.Query = parameters.ToString();
            return response.WithPayUrl<T>(uriBuilder.Uri);
        }
        
        /// <summary>
        /// Get API client fingerprint.
        /// </summary>
        /// <returns>The fingerprint.</returns>
        [ComVisible(true)]
        public IFingerprint GetFingerprint()
        {
            return _fingerprint;
        }
        
        /// <summary>
        /// Invoice Issue on Pay Form.
        /// https://developer.qiwi.com/en/bill-payments/#http
        /// </summary>
        /// <param name="paymentInfo">The invoice data.</param>
        /// <param name="customFields">The additional info.</param>
        /// <returns>The pay form URL.</returns>
        /// <exception cref="UrlEncodingException"></exception>
        [ComVisible(true)]
        public Uri CreatePaymentForm(PaymentInfo paymentInfo, CustomFields customFields = null)
        {
            var additional = customFields ?? new CustomFields();
            additional.ApiClient = _fingerprint.GetClientName();
            additional.ApiClientVersion = _fingerprint.GetClientVersion();
            var uriBuilder = new UriBuilder("https://oplata.qiwi.com/create");
            try
            {
                var parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
                parameters["amount"] = paymentInfo.Amount.ValueString;
                foreach (var keyValuePair in additional.ToDictionary())
                {
                    if (null != keyValuePair.Value)
                    {
                        parameters["customFields[" + keyValuePair.Key + "]"] = keyValuePair.Value.ToString();
                    }
                }
                parameters["publicKey"] = paymentInfo.PublicKey;
                parameters["billId"] = paymentInfo.BillId;
                if (null != paymentInfo.SuccessUrl)
                {
                    parameters["successUrl"] = paymentInfo.SuccessUrl.ToString();
                }

                uriBuilder.Query = parameters.ToString();
                return uriBuilder.Uri;
            }
            catch (System.Exception e)
            {
                throw new UrlEncodingException(e);
            }
        }
        
        /// <summary>
        /// Invoice issue by API.
        /// https://developer.qiwi.com/en/bill-payments/#create
        /// </summary>
        /// <param name="info">The invoice data.</param>
        /// <param name="customFields">The additional fields.</param>
        /// <returns>The invoice.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public BillResponse CreateBill(CreateBillInfo info, CustomFields customFields = null)
        {
            return CreateBill<BillResponse>(info, customFields);
        }
        
        /// <summary>
        /// Invoice issue by API.
        /// https://developer.qiwi.com/en/bill-payments/#create
        /// </summary>
        /// <param name="info">The invoice data.</param>
        /// <param name="customFields">The additional info.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>The invoice.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public T CreateBill<T>(CreateBillInfo info, CustomFields customFields = null) where T : BillResponse
        {
            var additional = customFields ?? new CustomFields();
            additional.ApiClient = _fingerprint.GetClientName();
            additional.ApiClientVersion = _fingerprint.GetClientVersion();
            var response = _requestMappingIntercessor.Request<T>(
                "PUT",
                BillsUrl + info.BillId,
                _headers,
                info.GetCreateBillRequest(additional)
            );
            return null != info.SuccessUrl ? AppendSuccessUrl<T>(response, info.SuccessUrl) : response;
        }
        
        /// <summary>
        /// Checking the invoice status.
        /// https://developer.qiwi.com/en/bill-payments/#invoice-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <returns>The invoice.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public BillResponse GetBillInfo(string billId)
        {
            return GetBillInfo<BillResponse>(billId);
        }
        
        /// <summary>
        /// Checking the invoice status.
        /// https://developer.qiwi.com/en/bill-payments/#invoice-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>The invoice.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public T GetBillInfo<T>(string billId) where T : BillResponse
        {
            return _requestMappingIntercessor.Request<T>(
                "GET",
                BillsUrl + billId,
                _headers
            );
        }
        
        /// <summary>
        /// Cancelling the invoice.
        /// https://developer.qiwi.com/en/bill-payments/#cancel
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <returns>The invoice.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public BillResponse CancelBill(string billId)
        {
            return CancelBill<BillResponse>(billId);
        }
        
        /// <summary>
        /// Cancelling the invoice.
        /// https://developer.qiwi.com/en/bill-payments/#cancel
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>The invoice.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public T CancelBill<T>(string billId) where T : BillResponse
        {
            return _requestMappingIntercessor.Request<T>(
                "POST",
                BillsUrl + billId + "/reject",
                _headers
            );
        }
        
        /// <summary>
        /// Refund issue by API.
        /// https://developer.qiwi.com/en/bill-payments/#refund
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <param name="refundId">The refund identifier.</param>
        /// <param name="amount">The refund amount.</param>
        /// <returns>The refund.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public RefundResponse RefundBill(string billId, string refundId, MoneyAmount amount)
        {
            return RefundBill<RefundResponse>(billId, refundId, amount);
        }
        
        /// <summary>
        /// Refund issue by API.
        /// https://developer.qiwi.com/en/bill-payments/#refund
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <param name="refundId">The refund identifier.</param>
        /// <param name="amount">The refund amount.</param>
        /// <typeparam name="T">The result refund type.</typeparam>
        /// <returns>The refund.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public T RefundBill<T>(string billId, string refundId, MoneyAmount amount) where T : RefundResponse
        {
            return _requestMappingIntercessor.Request<T>(
                "PUT",
                BillsUrl + billId + "/refunds/" + refundId,
                _headers,
                new RefundBillRequest
                {
                    Amount = amount
                }
            );
        }
        
        /// <summary>
        /// Checking the refund status.
        /// https://developer.qiwi.com/en/bill-payments/#refund-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <param name="refundId">The refund identifier.</param>
        /// <returns>The refund.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public RefundResponse GetRefundInfo(string billId, string refundId)
        {
            return GetRefundInfo<RefundResponse>(billId, refundId);
        }
        
        /// <summary>
        /// Checking the refund status.
        /// https://developer.qiwi.com/en/bill-payments/#refund-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <param name="refundId">The refund identifier.</param>
        /// <typeparam name="T">The result refund type.</typeparam>
        /// <returns>The refund.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public T GetRefundInfo<T>(string billId, string refundId) where T : RefundResponse
        {
            return _requestMappingIntercessor.Request<T>(
                "GET",
                BillsUrl + billId + "/refunds/" + refundId,
                _headers
            );
        }
    }
}
