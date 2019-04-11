using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Qiwi.BillPayments.Client;

namespace Qiwi.BillPayments.Json.Newtonsoft
{
    /// <inheritdoc />
    /// <summary>
    ///     Newtonsoft JSON serializer mapper.
    ///     https://www.newtonsoft.com/json
    /// </summary>
    [ComVisible(true)]
    public class NewtonsoftMapper : IObjectMapper
    {
        /// <summary>
        ///     The JSON serializer settings.
        /// </summary>
        private readonly JsonSerializerSettings _settings;

        /// <inheritdoc />
        public NewtonsoftMapper() : this(
            new JsonSerializerSettings
            {
                DateFormatString = BillPaymentsClient.DateTimeFormat
            }
        )
        {
        }

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="settings">The JSON serializer settings.</param>
        public NewtonsoftMapper(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public string WriteValue(object entityOpt)
        {
            return JsonConvert.SerializeObject(entityOpt, _settings);
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public T ReadValue<T>(string entityOpt)
        {
            return JsonConvert.DeserializeObject<T>(entityOpt, _settings);
        }
    }
}