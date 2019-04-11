using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Qiwi.BillPayments.Model;

namespace Qiwi.BillPayments.Web
{
    /// <inheritdoc />
    /// <summary>
    ///     Net HTTP client.
    ///     https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netcore-2.0
    /// </summary>
    [ComVisible(true)]
    public class NetClient : IClient
    {
        /// <summary>
        ///     The HTTP client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <inheritdoc />
        /// <summary>
        ///     The constructor.
        /// </summary>
        public NetClient() : this(new HttpClient())
        {
        }

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="httpClient">The real HTTP client.</param>
        public NetClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public ResponseData Request(
            string method,
            string url,
            IReadOnlyDictionary<string, string> headers,
            string entityOpt = null
        )
        {
            return RequestAsync(method, url, headers, entityOpt).Result;
        }

        /// <inheritdoc />
        [ComVisible(true)]
        public async Task<ResponseData> RequestAsync(
            string method,
            string url,
            IReadOnlyDictionary<string, string> headers,
            string entityOpt = null
        )
        {
            var uri = new Uri(url);
            var propfindMethod = new HttpMethod(method);
            var propfindHttpRequestMessage = new HttpRequestMessage(propfindMethod, uri);
            var contentType = new ContentType("application/json");
            foreach (var keyValuePair in headers)
                switch (keyValuePair.Key.ToLower())
                {
                    case "accept":
                        propfindHttpRequestMessage.Headers.Accept.Add(
                            new MediaTypeWithQualityHeaderValue(keyValuePair.Value)
                        );
                        break;
                    case "content-type":
                        contentType = new ContentType(keyValuePair.Value);
                        break;
                    default:
                        propfindHttpRequestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                        break;
                }

            if (null != entityOpt)
                propfindHttpRequestMessage.Content = new StringContent(
                    entityOpt,
                    Encoding.GetEncoding(contentType.CharSet ?? "utf-8"),
                    contentType.MediaType
                );

            var propfindHttpResponseMessage = await _httpClient.SendAsync(propfindHttpRequestMessage);
            return new ResponseData
            {
                Body = Encoding.UTF8.GetString(propfindHttpResponseMessage.Content.ReadAsByteArrayAsync().Result),
                HttpStatus = propfindHttpResponseMessage.StatusCode
            };
        }
    }
}