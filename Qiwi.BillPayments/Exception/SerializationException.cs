using System;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Exception
{
    /// <inheritdoc />
    /// <summary>
    ///     The JSON serialisation error.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class SerializationException : System.Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="cause">The error cause.</param>
        public SerializationException(System.Exception cause) : base(cause.Message, cause)
        {
        }
    }
}