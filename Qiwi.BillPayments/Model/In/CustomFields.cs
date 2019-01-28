using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model.In
{
    /// <summary>
    /// The invoice additional data.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class CustomFields
    {
        /// <summary>
        /// The API client name.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "apiClient")]
        public string ApiClient
        {
            get;
            set;
        }
        
        /// <summary>
        /// The API client version.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "apiClientVersion")]
        public string ApiClientVersion
        {
            get;
            set;
        }
    }
}
