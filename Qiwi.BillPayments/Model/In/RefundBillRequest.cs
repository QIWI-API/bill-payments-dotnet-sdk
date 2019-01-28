using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model.In
{
    /// <summary>
    /// The refund request.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class RefundBillRequest
    {
        /// <summary>
        /// The refund amount.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "amount")]
        public MoneyAmount Amount
        {
            get;
            set;
        }
        
    }
}
