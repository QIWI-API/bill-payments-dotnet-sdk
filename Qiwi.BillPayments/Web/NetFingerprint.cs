using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Web
{
    /// <inheritdoc />
    /// <summary>
    /// The SDK API fingerprint.
    /// </summary>
    [ComVisible(true)]
    public class NetFingerprint : IFingerprint
    {
        /// <summary>
        /// The API client version.
        /// </summary>
        private readonly string _clientVersion;
        
        /// <inheritdoc />
        /// <summary>
        /// The constructor.
        /// </summary>
        public NetFingerprint() : this(Assembly.GetExecutingAssembly())
        {
        }
        
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="assembly">Client assembly.</param>
        public NetFingerprint(Assembly assembly)
        {
            _clientVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }
        
        /// <inheritdoc />
        [ComVisible(true)]
        public string GetClientName()
        {
            return "dotnet_sdk";
        }
        
        /// <inheritdoc />
        [ComVisible(true)]
        public string GetClientVersion()
        {
            return _clientVersion;
        }
    }
}
