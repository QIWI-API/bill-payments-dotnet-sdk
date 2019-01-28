using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model
{
    /// <summary>
    /// The invoice payment notification.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class Notification
    {
        /// <summary>
        /// The invoice.
        /// </summary>
        [ComVisible(true)]
        [DataMember]
        public Bill Bill
        {
            get;
            set;
        }
        
        /// <summary>
        /// The notification version.
        /// </summary>
        [ComVisible(true)]
        [DataMember]
        public string Version
        {
            get;
            set;
        }
    }
}
