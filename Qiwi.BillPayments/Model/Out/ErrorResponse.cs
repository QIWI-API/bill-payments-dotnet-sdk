using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Model.Out
{
    /// <summary>
    /// The error response.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class ErrorResponse
    {
        /// <summary>
        /// The service name.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "serviceName")]
        public string ServiceName
        {
            get;
            set;
        }
        
        /// <summary>
        /// The error code.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "errorCode")]
        public string ErrorCode
        {
            get;
            set;
        }
        
        /// <summary>
        /// The description.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "description")]
        public string Description
        {
            get;
            set;
        }
        
        /// <summary>
        /// The user message.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "userMessage")]
        public string UserMessage
        {
            get;
            set;
        }
        
        /// <summary>
        /// The datetime.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "datetime")]
        public DateTime Datetime
        {
            get;
            set;
        }
        
        /// <summary>
        /// The trace ID.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "traceId")]
        public string TraceId
        {
            get;
            set;
        }
    }
}
