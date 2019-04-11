using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    ///     The invoice info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class Bill : FieldsDictionary
    {
        /// <summary>
        ///     The merchant’s site identifier.
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
        public BillStatus Status { get; set; }
    }
}