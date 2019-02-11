using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Web
{
    /// <summary>
    /// Fingerprint interface.
    /// </summary>
    [ComVisible(true)]
    public interface IFingerprint
    {
        /// <summary>
        /// Get the API client name.
        /// </summary>
        /// <returns>The API client name.</returns>
        [ComVisible(true)]
        string GetClientName();
        
        /// <summary>
        /// Get the API client version.
        /// </summary>
        /// <returns>The API client version.</returns>
        [ComVisible(true)]
        string GetClientVersion();
    }
}
