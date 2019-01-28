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
        /// The API datetime format.
        /// </summary>
        public const string DatetimeFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffzzz";
        
        private readonly RequestMappingIntercessor requestMappingIntercessor;
        
        private readonly Dictionary<string, string> headers;
        
        private readonly IFingerprint fingerprint;
        
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
            headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"},
                {"Accept", "application/json"},
                {"Authorization", "Bearer " + secretKey}
            };
            this.requestMappingIntercessor = requestMappingIntercessor;
            this.fingerprint = fingerprint;
        }
        
        /// <summary>
        /// Append success URL parameter to payment URL in invoice.
        /// </summary>
        /// <param name="response">The invoice.</param>
        /// <param name="successUrl">The success URL.</param>
        /// <returns>New invoice witch updated payment URL.</returns>
        [ComVisible(true)]
        public static BillResponse appendSuccessUrl(BillResponse response, Uri successUrl)
        {
            return appendSuccessUrl<BillResponse>(response, successUrl);
        }
        
        /// <summary>
        /// Append success URL parameter to payment URL in invoice.
        /// </summary>
        /// <param name="response">The invoice.</param>
        /// <param name="successUrl">The success URL.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>New invoice witch updated payment URL.</returns>
        [ComVisible(true)]
        public static T appendSuccessUrl<T>(BillResponse response, Uri successUrl) where T : BillResponse
        {
            var uriBuilder = new UriBuilder(response.PayUrl);
            var parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            parameters["successUrl"] = successUrl.ToString();
            uriBuilder.Query = parameters.ToString();
            return response.withPayUrl<T>(uriBuilder.Uri);
        }
        
        /// <summary>
        /// Get API client fingerprint.
        /// </summary>
        /// <returns>The fingerprint.</returns>
        [ComVisible(true)]
        public IFingerprint getFingerprint()
        {
            return fingerprint;
        }
        
        /// <summary>
        /// Invoice Issue on Pay Form.
        /// https://developer.qiwi.com/en/bill-payments/#http
        /// </summary>
        /// <param name="paymentInfo">The invoice data.</param>
        /// <returns>The pay form URL.</returns>
        /// <exception cref="UrlEncodingException"></exception>
        [ComVisible(true)]
        public Uri createPaymentForm(PaymentInfo paymentInfo)
        {
            try
            {
                var uriBuilder = new UriBuilder("https://oplata.qiwi.com/create");
                var parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
                parameters["amount"] = paymentInfo.Amount.ValueString;
                parameters["customFields[apiClient]"] = fingerprint.getClientName();
                parameters["customFields[apiClientVersion]"] = fingerprint.getClientVersion();
                parameters["publicKey"] = paymentInfo.PublicKey;
                parameters["billId"] = paymentInfo.BillId;
                parameters["successUrl"] = paymentInfo.SuccessUrl.ToString();
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
        /// <returns>The invoice.</returns>
        [ComVisible(true)]
        public BillResponse createBill(CreateBillInfo info)
        {
            return createBill<BillResponse>(info);
        }
        
        /// <summary>
        /// Invoice issue by API.
        /// https://developer.qiwi.com/en/bill-payments/#create
        /// </summary>
        /// <param name="info">The invoice data.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>The invoice.</returns>
        [ComVisible(true)]
        public T createBill<T>(CreateBillInfo info) where T : BillResponse
        {
            var response = requestMappingIntercessor.request<T>(
                "PUT",
                BillsUrl + info.BillId,
                headers,
                new CreateBillRequest
                {
                    Amount = info.Amount,
                    Comment = info.Comment,
                    ExpirationDateTime = info.ExpirationDateTime,
                    Customer = info.Customer,
                    CustomFields = new CustomFields
                    {
                        ApiClient = fingerprint.getClientName(),
                        ApiClientVersion = fingerprint.getClientVersion()
                    }
                }
            );
            return appendSuccessUrl<T>(response, info.SuccessUrl);
        }
        
        /// <summary>
        /// Checking the invoice status.
        /// https://developer.qiwi.com/en/bill-payments/#invoice-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <returns>The invoice.</returns>
        [ComVisible(true)]
        public BillResponse getBillInfo(string billId)
        {
            return getBillInfo<BillResponse>(billId);
        }
        
        /// <summary>
        /// Checking the invoice status.
        /// https://developer.qiwi.com/en/bill-payments/#invoice-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>The invoice.</returns>
        [ComVisible(true)]
        public T getBillInfo<T>(string billId) where T : BillResponse
        {
            return requestMappingIntercessor.request<T>(
                "GET",
                BillsUrl + billId,
                headers
            );
        }
        
        /// <summary>
        /// Cancelling the invoice.
        /// https://developer.qiwi.com/en/bill-payments/#cancel
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <returns>The invoice.</returns>
        [ComVisible(true)]
        public BillResponse cancelBill(string billId)
        {
            return cancelBill<BillResponse>(billId);
        }
        
        /// <summary>
        /// Cancelling the invoice.
        /// https://developer.qiwi.com/en/bill-payments/#cancel
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <typeparam name="T">The result invoice type.</typeparam>
        /// <returns>The invoice.</returns>
        [ComVisible(true)]
        public T cancelBill<T>(string billId) where T : BillResponse
        {
            return requestMappingIntercessor.request<T>(
                "POST",
                BillsUrl + billId + "/reject",
                headers
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
        [ComVisible(true)]
        public RefundResponse refundBill(string billId, string refundId, MoneyAmount amount)
        {
            return refundBill<RefundResponse>(billId, refundId, amount);
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
        [ComVisible(true)]
        public T refundBill<T>(string billId, string refundId, MoneyAmount amount) where T : RefundResponse
        {
            return requestMappingIntercessor.request<T>(
                "PUT",
                BillsUrl + billId + "/refunds/" + refundId,
                headers,
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
        [ComVisible(true)]
        public RefundResponse getRefundInfo(string billId, string refundId)
        {
            return getRefundInfo<RefundResponse>(billId, refundId);
        }
        
        /// <summary>
        /// Checking the refund status.
        /// https://developer.qiwi.com/en/bill-payments/#refund-status
        /// </summary>
        /// <param name="billId">The invoice identifier.</param>
        /// <param name="refundId">The refund identifier.</param>
        /// <typeparam name="T">The result refund type.</typeparam>
        /// <returns>The refund.</returns>
        [ComVisible(true)]
        public T getRefundInfo<T>(string billId, string refundId) where T : RefundResponse
        {
            return requestMappingIntercessor.request<T>(
                "GET",
                BillsUrl + billId + "/refunds/" + refundId,
                headers
            );
        }
    }
}
