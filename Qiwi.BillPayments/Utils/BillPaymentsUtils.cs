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
    ///     Util helper for QIWI Universal Payment Protocol API.
    /// </summary>
    [ComVisible(true)]
    public static class BillPaymentsUtils
    {
        /// <summary>
        ///     Even amount value to API format.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The evened amount value.</returns>
        [ComVisible(true)]
        public static decimal EvenValue(decimal value)
        {
            return decimal.Round(value, 2, MidpointRounding.ToEven);
        }

        /// <summary>
        ///     Even amount value to API format.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The evened amount value.</returns>
        [ComVisible(true)]
        public static decimal EvenValue(string value)
        {
            return EvenValue(Convert.ToDecimal(value, CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///     Even and format amount value to API format.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The formatted amount value.</returns>
        [ComVisible(true)]
        public static string FormatValue(decimal value)
        {
            return EvenValue(value).ToString("0.00", CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Even and format amount value to API.
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <returns>The formatted amount value.</returns>
        [ComVisible(true)]
        public static string FormatValue(string value)
        {
            return EvenValue(value).ToString("0.00", CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Parse API formatted dateTime.
        /// </summary>
        /// <param name="dateTime">The dateTime.</param>
        /// <returns>The dateTime.</returns>
        [ComVisible(true)]
        public static DateTime ParseDate(string dateTime)
        {
            return DateTime.Parse(
                dateTime,
                DateTimeFormatInfo.InvariantInfo,
                DateTimeStyles.AdjustToUniversal
            );
        }

        /// <summary>
        ///     Format dateTime to API.
        /// </summary>
        /// <param name="dateTime">The dateTime.</param>
        /// <returns>The dateTime.</returns>
        [ComVisible(true)]
        public static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString(BillPaymentsClient.DateTimeFormat, DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        ///     Make timeout dateTime in days from now.
        /// </summary>
        /// <param name="days">The days count.</param>
        /// <returns>Format dateTime to API.</returns>
        [ComVisible(true)]
        public static DateTime GetTimeoutDate(double? days = null)
        {
            return ParseDate(FormatDate(DateTime.Now.AddDays(days ?? 45)));
        }

        /// <summary>
        ///     Check invoice payment notifications.
        ///     https://developer.qiwi.com/en/bill-payments/#notification
        /// </summary>
        /// <param name="signature">The notification signature.</param>
        /// <param name="notification">The notification.</param>
        /// <param name="merchantSecret">The merchant secret key.</param>
        /// <returns>Check result.</returns>
        /// <exception cref="EncryptionException">On compute hash fail.</exception>
        [ComVisible(true)]
        public static bool CheckNotificationSignature(
            string signature,
            Notification notification,
            string merchantSecret
        )
        {
            var hash = Encrypt(merchantSecret, JoinFields(notification)).ToLower();
            return hash == signature;
        }

        /// <summary>
        ///     Join notification fields on string.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        /// <returns>The string data.</returns>
        private static string JoinFields(Notification notification)
        {
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

        /// <summary>
        ///     Get encryption hash.
        /// </summary>
        /// <param name="key">The encryption key.</param>
        /// <param name="data">The string data.</param>
        /// <returns>The hash.</returns>
        /// <exception cref="EncryptionException">On compute hash fail.</exception>
        private static string Encrypt(string key, string data)
        {
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