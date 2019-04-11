using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.Out
{
    /// <inheritdoc />
    /// <summary>
    ///     The invoice response.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class BillResponse : FieldsDictionary
    {
        /// <summary>
        ///     The merchant’s site identifier in API.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "siteId")]
        public string SiteId { get; set; }

        /// <summary>
        ///     The unique invoice identifier in the merchant’s system.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "billId")]
        public string BillId { get; set; }

        /// <summary>
        ///     The invoice amount info.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "amount")]
        public MoneyAmount Amount { get; set; }

        /// <summary>
        ///     The invoice status info.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "status")]
        public ResponseStatus Status { get; set; }

        /// <summary>
        ///     The comment to the invoice.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        ///     The customer info.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        /// <summary>
        ///     The dateTime of the invoice creation.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "creationDateTime")]
        public DateTime CreationDateTime { get; set; }

        /// <summary>
        ///     The expiration date of the pay form link.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "expirationDateTime")]
        public DateTime ExpirationDateTime { get; set; }

        /// <summary>
        ///     The pay form link.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "payUrl")]
        public Uri PayUrl { get; set; }

        /// <summary>
        ///     The invoice additional data.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "customFields")]
        public CustomFields CustomFields { get; set; }

        /// <summary>
        ///     Update pay form link.
        /// </summary>
        /// <param name="payUrl">The new pay form link.</param>
        /// <typeparam name="T">The new invoice response type.</typeparam>
        /// <returns>The new invoice response.</returns>
        [ComVisible(true)]
        public T WithPayUrl<T>(Uri payUrl) where T : BillResponse
        {
            var billResponse = (T) Activator.CreateInstance(typeof(T));
            billResponse.SiteId = SiteId;
            billResponse.BillId = BillId;
            billResponse.Amount = Amount;
            billResponse.Status = Status;
            billResponse.Comment = Comment;
            billResponse.Customer = Customer;
            billResponse.CreationDateTime = CreationDateTime;
            billResponse.ExpirationDateTime = ExpirationDateTime;
            billResponse.PayUrl = payUrl;
            billResponse.CustomFields = CustomFields;
            return billResponse;
        }
    }
}