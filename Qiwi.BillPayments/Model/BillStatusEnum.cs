using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    /// The invoice status enum.
    /// https://developer.qiwi.com/en/bill-payments/#refund-status
    /// </summary>
    [ComVisible(true)]
    public class BillStatusEnum : StringEnum<BillStatusEnum>
    {
        
        [ComVisible(true)]
        public static BillStatusEnum Waiting => new BillStatusEnum("WAITING");
        
        [ComVisible(true)]
        public static BillStatusEnum Paid => new BillStatusEnum("PAID");
        
        [ComVisible(true)]
        public static BillStatusEnum Rejected => new BillStatusEnum("REJECTED");
        
        [ComVisible(true)]
        public static BillStatusEnum Expired => new BillStatusEnum("EXPIRED");
        
        private BillStatusEnum(string value) : base(value)
        {
        }
    }
}
