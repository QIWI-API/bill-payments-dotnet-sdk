using System.Collections.Generic;
using System.Runtime.InteropServices;
using Qiwi.BillPayments.Exception;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Web;

namespace Qiwi.BillPayments.Client
{
    /// <summary>
    /// API request mapper.
    /// </summary>
    [ComVisible(true)]
    public class RequestMappingIntercessor
    {
        private readonly IObjectMapper mapper;
        
        private readonly IClient client;
        
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="client">The HTTP protocol mapper.</param>
        /// <param name="mapper">The JSON object mapper.</param>
        public RequestMappingIntercessor(IClient client, IObjectMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }
        
        /// <summary>
        /// Make the API request.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="url">The endpoint URL.</param>
        /// <param name="headers">The HTTP headers.</param>
        /// <param name="entityOpt">The request body.</param>
        /// <typeparam name="T">The result type.</typeparam>
        /// <returns>The response HTTP body.</returns>
        [ComVisible(true)]
        public T request<T>(
            string method,
            string url,
            Dictionary<string, string> headers,
            object entityOpt = null
        )
        {
            var jsonOpt = serializeRequestBody(entityOpt);
            var response = client.request(method, url, headers, jsonOpt);
            return deserializeResponseBody<T>(response);
        }
        
        private string serializeRequestBody(object entityOpt = null)
        {
            try
            {
                return null != entityOpt ? mapper.writeValue(entityOpt) : null;
            }
            catch (System.Exception exception)
            {
                throw new SerializationException(exception);
            }
        }
        
        private T deserializeResponseBody<T>(ResponseData response)
        {
            try
            {
                if (null == response.Body) {
                    throw new BadResponseException(response.HttpStatus);
                }
                return mapper.readValue<T>(response.Body);
            }
            catch (System.Exception)
            {
                throw mapToError(response);
            }
        }
        
        private BillPaymentsServiceException mapToError(ResponseData response)
        {
            try
            {
                var errorResponse = mapper.readValue<ErrorResponse>(response.Body);
                return new BillPaymentsServiceException(errorResponse, response.HttpStatus);
            }
            catch (System.Exception)
            {
                throw new BadResponseException(response);
            }
        }
    }
}
