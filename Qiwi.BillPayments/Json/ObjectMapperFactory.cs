using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Json
{
    /// <inheritdoc />
    /// <summary>
    ///     Factory for JSON mapper.
    /// </summary>
    [ComVisible(true)]
    public class ObjectMapperFactory : AbstractFactory<IObjectMapper, ContractObjectMapper>
    {
    }
}