using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    /// The invoice status info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class BillStatus : FieldsDictionary
    {
        /// <summary>
        /// The status refresh date.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "dateTime")]
        public DateTime DateTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// The invoice status value alias.
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
