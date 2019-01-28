using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model
{
    /// <summary>
    /// The HTTP response.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class ResponseData
    {
        /// <summary>
        /// The HTTP body.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "body")]
        public string Body
        {
            get;
            set;
        }
        
        /// <summary>
        /// The HTTP code.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "code")]
        public HttpStatusCode HttpStatus
        {
            get;
            set;
        }
    }
}
