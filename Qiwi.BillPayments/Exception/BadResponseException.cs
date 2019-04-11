using System;
using System.Net;
using System.Runtime.InteropServices;
using Qiwi.BillPayments.Model;

namespace Qiwi.BillPayments.Exception
{
    /// <inheritdoc />
    /// <summary>
    ///     Bad response from API.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class BadResponseException : System.Exception
    {
        /// <summary>
        ///     The response HTTP status code.
        /// </summary>
        [ComVisible(true)] public readonly HttpStatusCode HttpStatus;

        /// <inheritdoc />
        /// <summary>
        ///     THe constructor.
        /// </summary>
        /// <param name="responseData">The response data.</param>
        public BadResponseException(ResponseData responseData)
            : base("Http response code " + responseData.HttpStatus)
        {
            HttpStatus = responseData.HttpStatus;
        }

        /// <inheritdoc />
        /// <summary>
        ///     THe constructor.
        /// </summary>
        /// <param name="httpStatus">The response HTTP status.</param>
        public BadResponseException(HttpStatusCode httpStatus)
            : base("Empty body, HTTP status " + httpStatus)
        {
            HttpStatus = httpStatus;
        }
    }
}