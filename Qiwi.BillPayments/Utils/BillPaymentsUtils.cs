using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Exception;
using Qiwi.BillPayments.Model;

namespace Qiwi.BillPayments.Utils
{
    /// <summary>
    /// Util helper for QIWI Universal Payment Protocol API.
    /// </summary>
    [ComVisible(true)]
    public static class BillPaymentsUtils
    {
        /// <summary>
        /// Even amount value to API format.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The evened amount value.</returns>
        [ComVisible(true)]
        public static decimal evenValue(decimal value)
        {
            return decimal.Round(value, 2, MidpointRounding.ToEven);
        }
        
        /// <summary>
        /// Even amount value to API format.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The evened amount value.</returns>
        [ComVisible(true)]
        public static decimal evenValue(string value)
        {
            return evenValue(Convert.ToDecimal(value, CultureInfo.InvariantCulture));
        }
        
        /// <summary>
        /// Even and format amount value to API format.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The formatted amount value.</returns>
        [ComVisible(true)]
        public static string formatValue(decimal value)
        {
            return evenValue(value).ToString("0.00", CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Even and format amount value to API.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The formatted amount value.</returns>
        [ComVisible(true)]
        public static string formatValue(string value)
        {
            return evenValue(value).ToString("0.00", CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Parse API formatted datetime.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>The datetime.</returns>
        [ComVisible(true)]
        public static DateTime parseDate(string datetime)
        {
            return DateTime.ParseExact(datetime, BillPaymentsClient.DatetimeFormat, CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Format datetime to API.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>The datetime.</returns>
        [ComVisible(true)]
        public static string formatDate(DateTime datetime)
        {
            return datetime.ToString(BillPaymentsClient.DatetimeFormat);
        }
        
        /// <summary>
        /// Make timeout datetime in days from now.
        /// </summary>
        /// <param name="days">The days count.</param>
        /// <returns>Format datetime to API.</returns>
        [ComVisible(true)]
        public static DateTime getTimeoutDate(double? days = null)
        {
            return parseDate(formatDate(DateTime.Now.AddDays(days ?? 45)));
        }
        
        /// <summary>
        /// Check invoice payment notifications. 
        /// https://developer.qiwi.com/en/bill-payments/#notification
        /// </summary>
        /// <param name="signature">The notification signature.</param>
        /// <param name="notification">The notification.</param>
        /// <param name="merchantSecret">The merchant secret key.</param>
        /// <returns>Check result.</returns>
        [ComVisible(true)]
        public static bool checkNotificationSignature(
            string signature,
            Notification notification,
            string merchantSecret
        )
        {
            var hash = encrypt(merchantSecret, joinFields(notification)).ToLower();
            return hash == signature;
        }
        
        private static string joinFields(Notification notification) {
            var fields = new SortedDictionary<string, string>
            {
                {"amount.currency", notification?.Bill?.Amount?.CurrencyString ?? ""},
                {"amount.value", notification?.Bill?.Amount?.ValueString ?? ""},
                {"billId", notification?.Bill?.BillId ?? ""},
                {"siteId", notification?.Bill?.SiteId ?? ""},
                {"status", notification?.Bill?.Status?.ValueString ?? ""}
            };
            return string.Join("|", fields.Values);
        }
        
        private static string encrypt(string key, string data) {
            try
            {
                var encoding = Encoding.GetEncoding("utf-8");
                var hmac = HMAC.Create("HMACSHA256");
                hmac.Key = encoding.GetBytes(key);
                var hash = hmac.ComputeHash(encoding.GetBytes(data));
                return string.Concat(Array.ConvertAll(hash, hex => hex.ToString("X2")));
            }
            catch (System.Exception e)
            {
                throw new EncryptionException(e);
            }
        }
    }
}
