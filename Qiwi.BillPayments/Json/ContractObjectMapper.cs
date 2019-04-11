using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Qiwi.BillPayments.Client;

namespace Qiwi.BillPayments.Json
{
    /// <inheritdoc />
    /// <summary>
    ///     Data contract JSON serializer mapper.
    ///     https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.json.datacontractjsonserializer?view=netcore-2.0
    /// </summary>
    [ComVisible(true)]
    public class ContractObjectMapper : IObjectMapper
    {
        /// <summary>
        ///     The DataContract JSON serializer settings.
        /// </summary>
        private readonly DataContractJsonSerializerSettings _settings;

        /// <inheritdoc />
        /// <summary>
        ///     The constructor.
        /// </summary>
        public ContractObjectMapper() : this(
            new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat(BillPaymentsClient.DateTimeFormat, CultureInfo.InvariantCulture)
            }
        )
        {
        }

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="settings">The serializer settings.</param>
        public ContractObjectMapper(DataContractJsonSerializerSettings settings)
        {
            _settings = settings;
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public string WriteValue(object entityOpt)
        {
            var serializer = new DataContractJsonSerializer(entityOpt.GetType(), _settings);
            var stream = new MemoryStream();
            serializer.WriteObject(stream, entityOpt);
            var json = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return json;
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public T ReadValue<T>(string body)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), _settings);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(body));
            var rootObject = serializer.ReadObject(stream);
            stream.Close();
            return (T) rootObject;
        }
    }
}