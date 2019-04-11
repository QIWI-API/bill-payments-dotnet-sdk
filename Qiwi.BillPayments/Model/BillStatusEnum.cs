using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    ///     The invoice status enum.
    ///     https://developer.qiwi.com/en/bill-payments/#refund-status
    /// </summary>
    [ComVisible(true)]
    public class BillStatusEnum : StringEnum<BillStatusEnum>
    {
        /// <inheritdoc />
        private BillStatusEnum(string value) : base(value)
        {
        }

        /// <summary>
        ///     The invoice is waiting of pay.
        /// </summary>
        [ComVisible(true)]
        public static BillStatusEnum Waiting => new BillStatusEnum("WAITING");

        /// <summary>
        ///     The invoice is paid.
        /// </summary>
        [ComVisible(true)]
        public static BillStatusEnum Paid => new BillStatusEnum("PAID");

        /// <summary>
        ///     The invoice is rejected by client.
        /// </summary>
        [ComVisible(true)]
        public static BillStatusEnum Rejected => new BillStatusEnum("REJECTED");

        /// <summary>
        ///     The invoice is expired by waiting time limit.
        /// </summary>
        [ComVisible(true)]
        public static BillStatusEnum Expired => new BillStatusEnum("EXPIRED");
    }
}