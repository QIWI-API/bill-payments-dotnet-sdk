using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <summary>
    /// The invoice amount info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class MoneyAmount
    {
        /// <summary>
        /// The invoice amount value.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "value")]
        public string ValueString
        {
            get => BillPaymentsUtils.formatValue(ValueDecimal);
            set => ValueDecimal = BillPaymentsUtils.evenValue(value);
        }
        
        /// <summary>
        /// The invoice amount value.
        /// </summary>
        [ComVisible(true)]
        [IgnoreDataMember]
        public decimal ValueDecimal
        {
            get;
            set;
        }
        
        /// <summary>
        /// The invoice currency value.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "currency")]
        public string CurrencyString
        {
            get => CurrencyEnum.value;
            set => CurrencyEnum = CurrencyEnum.Parse(value);
        }
        
        /// <summary>
        /// The invoice currency value.
        /// </summary>
        [ComVisible(true)]
        [IgnoreDataMember]
        public CurrencyEnum CurrencyEnum
        {
            get;
            set;
        }
    }
}
