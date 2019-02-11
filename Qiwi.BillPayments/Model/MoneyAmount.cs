using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    /// The invoice amount info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class MoneyAmount : FieldsDictionary
    {
        /// <summary>
        /// The invoice amount value.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "value")]
        public string ValueString
        {
            get => BillPaymentsUtils.FormatValue(ValueDecimal);
            set => ValueDecimal = BillPaymentsUtils.EvenValue(value);
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
            get => CurrencyEnum.Value;
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
