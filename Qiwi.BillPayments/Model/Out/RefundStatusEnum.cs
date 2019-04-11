using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.Out
{
    /// <inheritdoc />
    /// <summary>
    ///     The refund status enum.
    ///     https://developer.qiwi.com/en/bill-payments/#refund-status
    /// </summary>
    [ComVisible(true)]
    public class RefundStatusEnum : StringEnum<RefundStatusEnum>
    {
        /// <inheritdoc />
        private RefundStatusEnum(string value) : base(value)
        {
        }

        /// <summary>
        ///     The partial refund of the invoice amount.
        /// </summary>
        [ComVisible(true)]
        public static RefundStatusEnum Partial => new RefundStatusEnum("PARTIAL");

        /// <summary>
        ///     The full refund of the invoice amount.
        /// </summary>
        [ComVisible(true)]
        public static RefundStatusEnum Full => new RefundStatusEnum("FULL");
    }
}