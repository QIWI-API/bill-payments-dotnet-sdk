using System;
using System.Collections.Generic;
using Qiwi.BillPayments.Model;

namespace Qiwi.BillPayments.Tests.Web
{
    public class FakeClientRequestEventArgs : EventArgs
    {
        public readonly int Counter;
        
        public readonly string Method;
        
        public readonly string Url;
        
        public readonly string EntityOpt;
        
        public readonly IReadOnlyDictionary<string, string> Headers;
        
        public ResponseData ResponseData { get; }
        
        public FakeClientRequestEventArgs(
            int counter,
            ResponseData defaultResponseData,
            string method,
            string url,
            IReadOnlyDictionary<string, string> headers,
            string entityOpt = null
        )
        {
            ResponseData = defaultResponseData;
            Counter = counter;
            Method = method;
            Url = url;
            Headers = headers;
            EntityOpt = entityOpt;
        }
    }
}
