using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Qiwi.BillPayments.Client;

namespace Qiwi.BillPayments.Json.Newtonsoft
{
    /// <inheritdoc />
    /// <summary>
    /// Newtonsoft JSON serializer mapper.
    /// https://www.newtonsoft.com/json
    /// </summary>
    [ComVisible(true)]
    public class NewtonsoftMapper : IObjectMapper
    {
        private readonly JsonSerializerSettings settings;
        
        /// <inheritdoc />
        public NewtonsoftMapper() : this(
            new JsonSerializerSettings
            {
                DateFormatString = BillPaymentsClient.DatetimeFormat
            }
        )
        {
        }
        
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="settings">The JSON serializer settings.</param>
        public NewtonsoftMapper(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }
        
        /// <inheritdoc />
        [ComVisible(true)]
        public string writeValue(object entityOpt)
        {
            return JsonConvert.SerializeObject(entityOpt, settings);
        }
        
        /// <inheritdoc />
        [ComVisible(true)]
        public T readValue<T>(string entityOpt)
        {
            return JsonConvert.DeserializeObject<T>(entityOpt, settings);
        }
    }
}
