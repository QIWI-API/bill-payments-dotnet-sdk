using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.In
{
    /// <inheritdoc />
    /// <summary>
    ///     Create issue info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class CreateBillInfo : FieldsDictionary
    {
        /// <summary>
        ///     The unique invoice identifier in merchant's system.
        ///     Example:
        ///     Guid.NewGuid().ToString()
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
        ///     The invoice commentary.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        ///     The invoice due date.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "expirationDateTime")]
        public DateTime ExpirationDateTime { get; set; }

        /// <summary>
        ///     The customer's info.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        /// <summary>
        ///     The URL to which the client will be redirected in case of successful payment.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "successUrl")]
        public Uri SuccessUrl { get; set; }

        /// <summary>
        ///     Get the create invoice request.
        /// </summary>
        /// <param name="customFields">The invoice additional data.</param>
        /// <returns>The create invoice request.</returns>
        [ComVisible(true)]
        public CreateBillRequest GetCreateBillRequest(CustomFields customFields)
        {
            return new CreateBillRequest
            {
                Amount = Amount,
                Comment = Comment,
                ExpirationDateTime = ExpirationDateTime,
                Customer = Customer,
                CustomFields = customFields
            };
        }
    }
}