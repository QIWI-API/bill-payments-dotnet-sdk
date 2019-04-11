using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.In
{
    /// <inheritdoc />
    /// <summary>
    ///     Create issue request.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class CreateBillRequest : FieldsDictionary
    {
        /// <summary>
        ///     The invoice amount witch currency.
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
        ///     The invoice expiration date.
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
        ///     The invoice additional data.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "customFields")]
        public CustomFields CustomFields { get; set; }
    }
}