using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    ///     The invoice payment notification.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class Notification : FieldsDictionary
    {
        /// <summary>
        ///     The invoice.
        /// </summary>
        [ComVisible(true)]
        [DataMember]
        public Bill Bill { get; set; }

        /// <summary>
        ///     The notification version.
        /// </summary>
        [ComVisible(true)]
        [DataMember]
        public string Version { get; set; }
    }
}