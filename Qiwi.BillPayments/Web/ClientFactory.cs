using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Web
{
    /// <inheritdoc />
    /// <summary>
    /// Factory for HTTP mapper.
    /// </summary>
    [ComVisible(true)]
    public class ClientFactory : AbstractFactory<IClient, NetClient>
    {
    }
}
