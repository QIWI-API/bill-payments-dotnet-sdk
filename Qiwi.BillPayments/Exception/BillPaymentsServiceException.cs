using System;
using System.Net;
using System.Runtime.InteropServices;
using Qiwi.BillPayments.Model.Out;

namespace Qiwi.BillPayments.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// The API error response.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class BillPaymentsServiceException : System.Exception
    {
        /// <summary>
        /// The API response.
        /// </summary>
        [ComVisible(true)]
        public readonly ErrorResponse response;
        
        /// <summary>
        /// The HTTP status code.
        /// </summary>
        [ComVisible(true)]
        public readonly HttpStatusCode httpStatus;
        
        /// <inheritdoc />
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="response">The API response.</param>
        /// <param name="httpStatus">The HTTP status code.</param>
        public BillPaymentsServiceException(ErrorResponse response, HttpStatusCode httpStatus)
            : base(response.UserMessage)
        {
            this.response = response;
            this.httpStatus = httpStatus;
        }
    }
}
