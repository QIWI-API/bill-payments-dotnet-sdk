using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model
{
    /// <summary>
    /// The invoice status info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class BillStatus
    {
        /// <summary>
        /// The status refresh date.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "datetime")]
        public DateTime Datetime
        {
            get;
            set;
        }
        
        /// <summary>
        /// The invoice status value.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "value")]
        public string ValueString
        {
            get => ValueEnum.ToString();
            set => ValueEnum = BillStatusEnum.Parse(value);
        }
        
        /// <summary>
        /// The invoice status value.
        /// </summary>
        [ComVisible(true)]
        [IgnoreDataMember]
        public BillStatusEnum ValueEnum
        {
            get;
            set;
        }
    }
}
