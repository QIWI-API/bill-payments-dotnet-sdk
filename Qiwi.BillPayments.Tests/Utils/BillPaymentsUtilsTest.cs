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
        public void TestFormatValue()
        {
            var value = BillPaymentsUtils.formatValue("200.345");
            Assert.AreEqual("200.34", value, "Equal format value");
        }

        [Test]
        public void TestGetTimeoutDate()
        {
            var value = BillPaymentsUtils.getTimeoutDate();
            Assert.IsTrue(DateTime.Now < value, "Timeout date in future");
            Assert.AreEqual(45, (value - DateTime.Now).Days + 1, "Timeout date offset");   
        }
        
        [Test]
        public void TestCheckNotificationSignature()
        {
            const string merchantSecret = "test-merchant-secret-for-signature-check";
            const string signature = "07e0ebb10916d97760c196034105d010607a6c6b7d72bfa1c3451448ac484a3b";
            var notification = new Notification
            {
                Bill = new Bill
                {
                    SiteId = "test",
                    BillId = "test_bill",
                    Amount = new MoneyAmount
                    {
                        ValueString = "1.00",
                        CurrencyString = "RUB"
                    },
                    Status = new BillStatus
                    {
                        ValueString = "PAID"
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
