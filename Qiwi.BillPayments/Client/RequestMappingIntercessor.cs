using System;
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
        /// <summary>
        /// The JSON converter.
        /// </summary>
        private readonly IObjectMapper _mapper;
        
        /// <summary>
        /// The HTTP client. 
        /// </summary>
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
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
        /// <exception cref="HttpClientException">On request fail.</exception>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        [ComVisible(true)]
        public T Request<T>(
            string method,
            string url,
            Dictionary<string, string> headers,
            object entityOpt = null
        )
        {
            var jsonOpt = SerializeRequestBody(entityOpt);
            ResponseData response;
            try
            {
                response = _client.Request(method, url, headers, jsonOpt);
            }
            catch (System.Exception exception)
            {
                throw new HttpClientException(exception);
            }

            return DeserializeResponseBody<T>(response);
        }
        
        /// <summary>
        /// Convert request body object to JSON.
        /// </summary>
        /// <param name="entityOpt">The body object.</param>
        /// <returns>The JSON.</returns>
        /// <exception cref="SerializationException">On request body serialization fail.</exception>
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
        
        /// <summary>
        /// Convert response body JSON to object.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object.</returns>
        /// <exception cref="BadResponseException">On response parse fail.</exception>
        /// <exception cref="BillPaymentsServiceException">On API return error message.</exception>
        private T DeserializeResponseBody<T>(ResponseData response)
        {
            if (string.IsNullOrEmpty(response.Body)) {
                throw new BadResponseException(response.HttpStatus);
            }
            try
            {
                return _mapper.ReadValue<T>(response.Body);
            }
            catch (System.Exception)
            {
                throw MapToError(response);
            }
        }
        
        /// <summary>
        /// Convert error response body JSON to object. 
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The error object.</returns>
        /// <exception cref="BadResponseException">On error message parse fail.</exception>
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
