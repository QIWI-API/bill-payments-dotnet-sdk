using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.In
{
    /// <inheritdoc />
    /// <summary>
    ///     Invoice data are put in Pay Form URL.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class PaymentInfo : FieldsDictionary
    {
        /// <summary>
        ///     The merchant public key.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "publicKey")]
        public string PublicKey { get; set; }

        /// <summary>
        ///     The invoice amount.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "amount")]
        public MoneyAmount Amount { get; set; }

        /// <summary>
        ///     Unique invoice identifier in merchant’s system.
        ///     Example:
        ///     Guid.NewGuid().ToString()
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "billId")]
        public string BillId { get; set; }

        /// <summary>
        ///     The URL to which the client will be redirected in case of successful payment.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "successUrl")]
        public Uri SuccessUrl { get; set; }
    }
}