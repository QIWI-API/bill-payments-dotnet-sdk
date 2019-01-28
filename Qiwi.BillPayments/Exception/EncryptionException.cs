using System;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// The hash encryption error.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class EncryptionException : System.Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="cause">The error cause.</param>
        public EncryptionException(System.Exception cause) : base(cause.Message, cause)
        {
        }
    }
}
