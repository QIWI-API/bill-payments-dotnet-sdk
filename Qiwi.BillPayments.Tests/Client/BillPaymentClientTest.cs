using System;
using System.Web;
using NUnit.Framework;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model.Out;

namespace Qiwi.BillPayments.Tests.Client
{
    [TestFixture]
    public class BillPaymentClientTest
    {
        [Test]
        [Sequential]
        public void TestAppendSuccessUrl(
            [Values("https://oplata.qiwi.com/form/?invoice_uid=bb773791-9bd9-42c1-b8fc-3358cd108422")] string payUri,
            [Values("http://test.ru/")] string successUrl
        )
        {
            // Prepare
            var baseBillResponse = new BillResponse
            {
                PayUrl = new Uri(payUri)
            };
            // Test
            var billResponse = BillPaymentsClient.appendSuccessUrl(baseBillResponse, new Uri(successUrl));
            var query = HttpUtility.ParseQueryString(billResponse.PayUrl.Query);
            // Assert
            Assert.AreEqual(successUrl, query["successUrl"]);
        }
    }
}