using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Tests.Utils
{
    [TestClass]
    public class BillPaymentsUtilsTest
    {
        [TestMethod]
        [DataRow("200.345", "200.34")]
        public void TestFormatValue(string iValue, string oValue)
        {
            var value = BillPaymentsUtils.FormatValue(iValue);
            Assert.AreEqual(oValue, value, "Equal format value");
        }

        [TestMethod]
        [DataRow(45, null)]
        [DataRow(1, 1.0)]
        public void TestGetTimeoutDate(int offset, double? days = null)
        {
            var value = BillPaymentsUtils.GetTimeoutDate(days);
            Assert.IsTrue(DateTime.Now < value, "Timeout date in future");
            Assert.AreEqual(offset, (value - DateTime.Now).Days + 1, "Timeout date offset");
        }

        [TestMethod]
        [DataRow(
            "test-merchant-secret-for-signature-check",
            "07e0ebb10916d97760c196034105d010607a6c6b7d72bfa1c3451448ac484a3b",
            "test",
            "test_bill",
            "1.00",
            "RUB",
            "PAID"
        )]
        public void TestCheckNotificationSignature(
            string merchantSecret,
            string signature,
            string siteId = null,
            string billId = null,
            string value = null,
            string currency = null,
            string status = null
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
                BillPaymentsUtils.CheckNotificationSignature("foo", notification, merchantSecret),
                "Invalid signature check fails"
            );
            Assert.IsTrue(
                BillPaymentsUtils.CheckNotificationSignature(signature, notification, merchantSecret),
                "Valid signature check success"
            );
        }
    }
}