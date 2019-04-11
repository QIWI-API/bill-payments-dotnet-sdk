using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.Out
{
    /// <inheritdoc />
    /// <summary>
    ///     The invoice status info.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class ResponseStatus : FieldsDictionary
    {
        /// <summary>
        ///     The status refresh dateTime.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "changedDateTime")]
        public DateTime ChangedDateTime { get; set; }

        /// <summary>
        ///     The status value.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "value")]
        public string ValueString
        {
            get => ValueEnum.ToString();
            set => ValueEnum = BillStatusEnum.Parse(value);
        }

        /// <summary>
        ///     The status value.
        /// </summary>
        [ComVisible(true)]
        [IgnoreDataMember]
        public BillStatusEnum ValueEnum { get; set; }
    }
}