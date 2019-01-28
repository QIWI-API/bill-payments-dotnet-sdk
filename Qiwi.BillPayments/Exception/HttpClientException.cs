using System;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// The HTTP client error.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class HttpClientException: System.Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="cause">The error cause.</param>
        public HttpClientException(System.Exception cause) : base(cause.Message, cause)
        {
        }
    }
}
