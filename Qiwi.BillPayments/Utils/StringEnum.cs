using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Utils
{
    /// <inheritdoc />
    /// <summary>
    /// String enum abstraction.
    /// </summary>
    /// <typeparam name="T">The string enum type.</typeparam>
    [ComVisible(true)]
    public abstract class StringEnum<T> : IEquatable<T> where T : StringEnum<T>
    {
        /// <summary>
        /// The enum item value.
        /// </summary>
        public readonly string value;
        
        /// <summary>
        /// The enum item constructor.
        /// </summary>
        /// <param name="value">The enum item value.</param>
        protected StringEnum(string value)
        {
            this.value = value;
        }
        
        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>The enum item value.</returns>
        [ComVisible(true)]
        public override string ToString()
        {
            return value;
        }
        
        /// <summary>
        /// Get all enum items.
        /// </summary>
        /// <returns>The enum items.</returns>
        [ComVisible(true)]
        public static List<T> AsList()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(T))
                .Select(p => (T) p.GetValue(null))
                .ToList();
        }
        
        /// <summary>
        /// String to enum item.
        /// </summary>
        /// <param name="value">The enum item value.</param>
        /// <returns></returns>
        [ComVisible(true)]
        public static T Parse(string value)
        {
            var all = AsList();
            return all.All(a => a.value != value) ? null : all.Single(a => a.value == value);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Compare enum items.
        /// </summary>
        /// <param name="other">The enum item value.</param>
        /// <returns>The comparison result.</returns>
        [ComVisible(true)]
        public bool Equals(T other)
        {
            if (other == null) return false;
            return value == other.value;
        }
        
        /// <summary>
        /// Compare enum items.
        /// </summary>
        /// <param name="obj">The enum item value.</param>
        /// <returns>The comparison result.</returns>
        [ComVisible(true)]
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case T other:
                    return Equals(other);
                default:
                    return false;
            }
        }
        
        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>The enum item value hash code.</returns>
        [ComVisible(true)]
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        
        /// <summary>
        /// The equals operator.
        /// </summary>
        /// <param name="a">The enum item.</param>
        /// <param name="b">The enum item.</param>
        /// <returns>The comparison result.</returns>
        [ComVisible(true)]
        public static bool operator ==(StringEnum<T> a, StringEnum<T> b)
        {
            return a?.Equals(b) ?? false;
        }
        
        /// <summary>
        /// The not equals operator.
        /// </summary>
        /// <param name="a">The enum item.</param>
        /// <param name="b">The enum item.</param>
        /// <returns>The comparison result.</returns>
        [ComVisible(true)]
        public static bool operator !=(StringEnum<T> a, StringEnum<T> b)
        {
            return !(a?.Equals(b) ?? false);
        }
    } 
}
