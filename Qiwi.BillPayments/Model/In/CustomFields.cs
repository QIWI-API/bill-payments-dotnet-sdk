using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model.In
{
    /// <inheritdoc />
    /// <summary>
    ///     The invoice additional data.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class CustomFields : FieldsDictionary
    {
        /// <summary>
        ///     The API client name.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "apiClient")]
        public string ApiClient { get; set; }

        /// <summary>
        ///     The API client version.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "apiClientVersion")]
        public string ApiClientVersion { get; set; }

        /// <summary>
        ///     The style theme code.
        /// </summary>
        [ComVisible(true)]
        [DataMember(Name = "themeCode")]
        public string ThemeCode { get; set; }
    }
}