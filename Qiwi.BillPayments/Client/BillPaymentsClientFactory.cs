using System.Runtime.InteropServices;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Web;

namespace Qiwi.BillPayments.Client
{
    /// <summary>
    ///     Factory for API client.
    /// </summary>
    [ComVisible(true)]
    public static class BillPaymentsClientFactory
    {
        /// <summary>
        ///     Make QIWI Universal Payment Protocol API client instance.
        /// </summary>
        /// <param name="secretKey">The merchant secret key.</param>
        /// <param name="client">The HTTP protocol mapper.</param>
        /// <param name="objectMapper">The JSON object mapper.</param>
        /// <param name="fingerprint">The client fingerprint.</param>
        /// <returns>The client instance.</returns>
        [ComVisible(true)]
        public static BillPaymentsClient Create(
            string secretKey,
            IClient client = null,
            IObjectMapper objectMapper = null,
            IFingerprint fingerprint = null
        )
        {
            return new BillPaymentsClient(
                secretKey,
                new RequestMappingIntercessor(
                    client ?? ClientFactory.Create(),
                    objectMapper ?? ObjectMapperFactory.Create()
                ),
                fingerprint ?? FingerprintFactory.Create()
            );
        }
    }
}