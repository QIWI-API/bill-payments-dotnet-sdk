using System;
using NUnit.Framework;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Tests.Utils
{
    [TestFixture]
    public class BillPaymentsUtilsTest
    {
        [Test]
        [Sequential]
        public void TestFormatValue(
            [Values("200.345")] string iValue,
            [Values("200.34")] string oValue
        )
        {
            var value = BillPaymentsUtils.formatValue(iValue);
            Assert.AreEqual(oValue, value, "Equal format value");
        }

        [Test]
        [Sequential]
        public void TestGetTimeoutDate(
            [Values(45,   1)]    int offset,
            [Values(null, 1.0d)] double? days
        )
        {
            var value = BillPaymentsUtils.getTimeoutDate(days);
            Assert.IsTrue(DateTime.Now < value, "Timeout date in future");
            Assert.AreEqual(offset, (value - DateTime.Now).Days + 1, "Timeout date offset");   
        }
        
        [Test]
        [Sequential]
        public void TestCheckNotificationSignature(
            [Values("test-merchant-secret-for-signature-check")] string merchantSecret,
            [Values("07e0ebb10916d97760c196034105d010607a6c6b7d72bfa1c3451448ac484a3b")] string signature,
            [Values("test")] string siteId,
            [Values("test_bill")] string billId,
            [Values("1.00")] string value,
            [Values("RUB")] string currency,
            [Values("PAID")] string status
        )
        {
            var notification = new Notification
            {
                Bill = new Bill
                {
                    SiteId = siteId,
                    BillId = billId,
                    Amount = new MoneyAmount
                    {
                        ValueString = value,
                        CurrencyString = currency
                    },
                    Status = new BillStatus
                    {
                        ValueString = status
                    }
                }
            };
            Assert.IsFalse(
                BillPaymentsUtils.checkNotificationSignature("foo", notification, merchantSecret),
                "Invalid signature check fails"
            );
            Assert.IsTrue(
                BillPaymentsUtils.checkNotificationSignature(signature, notification, merchantSecret),
                "Valid signature check success"
            );
        }
    }
}
