using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model.In
{
    /// <summary>
    /// Create issue request.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class CreateBillRequest
    {
        /// <summary>
        /// The invoice amount witch currency.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "amount")]
        public MoneyAmount Amount
        {
            get;
            set;
        }
        
        [ComVisible(true)]
        [DataMember(Name = "comment")]
        public string Comment
        {
            get;
            set;
        }
        
        /// <summary>
        /// The invoice commentary.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "expirationDateTime")]
        public DateTime ExpirationDateTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// The customer's info.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "customer")]
        public Customer Customer
        {
            get;
            set;
        }
        
        /// <summary>
        /// The invoice additional data.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "customFields")]
        public CustomFields CustomFields
        {
            get;
            set;
        }
    }
}
