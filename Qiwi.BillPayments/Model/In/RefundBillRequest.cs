using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.In
{
    /// <inheritdoc />
    /// <summary>
    ///     The refund request.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class RefundBillRequest : FieldsDictionary
    {
        /// <summary>
        ///     The refund amount.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "amount")]
        public MoneyAmount Amount { get; set; }
    }
}