using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Json
{
    /// <inheritdoc />
    /// <summary>
    ///     Data contract JSON serializer mapper.
    ///     Newtonsoft.Json
    /// </summary>
    [ComVisible(true)]
    public class ContractObjectMapper : IObjectMapper
    {

        /// <inheritdoc />
        /// <summary>
        ///     The constructor.
        /// </summary>
        public ContractObjectMapper()
        {

        }

        /// <inheritdoc />
        [ComVisible(true)]
        public string WriteValue(object entityOpt)
        {
            return JsonConvert.SerializeObject(entityOpt);
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public T ReadValue<T>(string body)
        {
            try
            {
                var value = JsonConvert.DeserializeObject<T>(body);
                return value;
            }

            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }
        }
    }
}