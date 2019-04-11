using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Web
{
    /// <inheritdoc />
    /// <summary>
    ///     Factory for client fingerprint.
    /// </summary>
    [ComVisible(true)]
    public class FingerprintFactory : AbstractFactory<IFingerprint, NetFingerprint>
    {
    }
}