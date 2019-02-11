using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Json
{
    /// <summary>
    /// JSON mapper interface.
    /// </summary>
    [ComVisible(true)]
    public interface IObjectMapper
    {
        /// <summary>
        /// Object to JSON.
        /// </summary>
        /// <param name="entityOpt">The object.</param>
        /// <returns>The JSON.</returns>
        [ComVisible(true)]
        string WriteValue(object entityOpt);
        
        /// <summary>
        /// JSON to object.
        /// </summary>
        /// <param name="body">The JSON.</param>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object.</returns>
        [ComVisible(true)]
        T ReadValue<T>(string body);
    }
}
