using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.Out
{
    /// <inheritdoc />
    /// <summary>
    /// The refund response.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class RefundResponse : FieldsDictionary
    {
        /// <summary>
        /// The invoice amount.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "amount")]
        public MoneyAmount Amount
        {
            get;
            set;
        }
        
        /// <summary>
        /// The dateTime of refund processing.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "dateTime")]
        public DateTime DateTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// Unique refund identifier in merchant’s system.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "refundId")]
        public string RefundId
        {
            get;
            set;
        }
        
        /// <summary>
        /// The refund status.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "status")]
        public string StatusString
        {
            get => StatusEnum.ToString();
            set => StatusEnum = RefundStatusEnum.Parse(value);
        }

        /// <summary>
        /// The refund status.
        /// </summary>
        [ComVisible(true)]
        [IgnoreDataMember]
        public RefundStatusEnum StatusEnum
        {
            get;
            set;
        }
    }
}
