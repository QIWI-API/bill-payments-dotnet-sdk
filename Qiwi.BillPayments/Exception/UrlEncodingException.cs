using System;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// The URL encoding error.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class UrlEncodingException: System.Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="cause">The error cause.</param>
        public UrlEncodingException(System.Exception cause) : base(cause.Message, cause)
        {
        }
    }
}
