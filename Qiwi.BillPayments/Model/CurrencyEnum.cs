using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Qiwi.BillPayments.Utils;

namespace Qiwi.BillPayments.Model
{
    /// <inheritdoc />
    /// <summary>
    /// Invoice currency enum.
    /// </summary>
    [ComVisible(true)]
    public class CurrencyEnum : StringEnum<CurrencyEnum>
    {
        /// <summary>
        /// Alpha-3 ISO 4217 codes of currencies.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, CurrencyEnum> Values = CultureInfo
            .GetCultures(CultureTypes.SpecificCultures)
            .Select(culture => new RegionInfo(culture.LCID).ISOCurrencySymbol)
            .Distinct()
            .OrderBy(code => code)
            .ToDictionary(code => code, code => new CurrencyEnum(code), StringComparer.OrdinalIgnoreCase);
        
        /// <inheritdoc />
        private CurrencyEnum(string value) : base(value)
        {
        }
        
        /// <summary>
        /// Get all enum items.
        /// </summary>
        /// <returns>The enum items.</returns>
        [ComVisible(true)]
        public new static List<CurrencyEnum> AsList()
        {
            return Values.Values.ToList();
        }
        
        /// <summary>
        /// String to enum item.
        /// </summary>
        /// <param name="value">The enum item value.</param>
        /// <returns></returns>
        [ComVisible(true)]
        public new static CurrencyEnum Parse(string value)
        {
            Values.TryGetValue(value, out var result);
            return result;
        }
    }
}
