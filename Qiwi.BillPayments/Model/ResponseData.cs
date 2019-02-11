using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    /// The HTTP response.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class ResponseData : FieldsDictionary
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
