using System.Collections.Generic;
using System.Collections.ObjectModel;
using Qiwi.BillPayments.Json;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Web;

namespace Qiwi.BillPayments.Tests.Web
{
    public class FakeClient : IClient
    {   
        public event FakeClientRequestHandler OnRequest;

        public IObjectMapper ObjectMapper { get; }

        public int RequestCounter { get; set; }
        
        public ResponseData DefaultResponseData { get; set; } = new ResponseData();
        
        public FakeClient(IObjectMapper objectMapper)
        {
            ObjectMapper = objectMapper;
        }
        
        public ResponseData request(
            string method,
            string url,
            IReadOnlyDictionary<string, string> headers,
            string entityOpt = null
        )
        {
            var args = new FakeClientRequestEventArgs(
                RequestCounter += 1,
                DefaultResponseData,
                method,
                url,
                headers,
                entityOpt
            );
            OnRequest?.Invoke(this, args);
            return args.ResponseData;
        }
    }
}
