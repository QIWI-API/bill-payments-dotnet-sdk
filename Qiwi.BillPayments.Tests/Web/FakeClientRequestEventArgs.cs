using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Qiwi.BillPayments.Model;

namespace Qiwi.BillPayments.Tests.Web
{
    public class FakeClientRequestEventArgs : EventArgs
    {
        public readonly int counter;
        
        public readonly string method;
        
        public readonly string url;
        
        public readonly string entityOpt;
        
        public readonly IReadOnlyDictionary<string, string> headers;
        
        public ResponseData ResponseData { get; set; }
        
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
            this.counter = counter;
            this.method = method;
            this.url = url;
            this.headers = headers;
            this.entityOpt = entityOpt;
        }
    }
}
