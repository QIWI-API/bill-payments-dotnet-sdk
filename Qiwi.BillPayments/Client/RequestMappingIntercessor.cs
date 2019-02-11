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
        private readonly IObjectMapper _mapper;
        
        private readonly IClient _client;
        
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="client">The HTTP protocol mapper.</param>
        /// <param name="mapper">The JSON object mapper.</param>
        public RequestMappingIntercessor(IClient client, IObjectMapper mapper)
        {
            _client = client;
            _mapper = mapper;
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
        public T Request<T>(
            string method,
            string url,
            Dictionary<string, string> headers,
            object entityOpt = null
        )
        {
            var jsonOpt = SerializeRequestBody(entityOpt);
            var response = _client.Request(method, url, headers, jsonOpt);
            return DeserializeResponseBody<T>(response);
        }
        
        private string SerializeRequestBody(object entityOpt = null)
        {
            try
            {
                return null != entityOpt ? _mapper.WriteValue(entityOpt) : null;
            }
            catch (System.Exception exception)
            {
                throw new SerializationException(exception);
            }
        }
        
        private T DeserializeResponseBody<T>(ResponseData response)
        {
            try
            {
                if (null == response.Body) {
                    throw new BadResponseException(response.HttpStatus);
                }
                return _mapper.ReadValue<T>(response.Body);
            }
            catch (System.Exception)
            {
                throw MapToError(response);
            }
        }
        
        private BillPaymentsServiceException MapToError(ResponseData response)
        {
            try
            {
                var errorResponse = _mapper.ReadValue<ErrorResponse>(response.Body);
                return new BillPaymentsServiceException(errorResponse, response.HttpStatus);
            }
            catch (System.Exception)
            {
                throw new BadResponseException(response);
            }
        }
    }
}
