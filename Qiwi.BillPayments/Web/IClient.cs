using System.Collections.Generic;
using Qiwi.BillPayments.Model;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Web
{
    /// <summary>
    /// HTTP client interface.
    /// </summary>
    [ComVisible(true)]
    public interface IClient
    {
        /// <summary>
        /// Make HTTP request.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="url">The endpoint URL.</param>
        /// <param name="headers">The HTTP headers.</param>
        /// <param name="entityOpt">The request body.</param>
        /// <returns>The response data.</returns>
        [ComVisible(true)]
        ResponseData request(
            string method,
            string url,
            IReadOnlyDictionary<string, string> headers,
            string entityOpt = null
        );
    }
}
