using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Qiwi.BillPayments.Utils
{
    /// <summary>
    ///     Dictionary of object fields container.
    /// </summary>
    [ComVisible(true)]
    [DataContract]
    public class FieldsDictionary
    {
        /// <summary>
        ///     Get dictionary of object fields.
        /// </summary>
        /// <returns>Dictionary of fields.</returns>
        [ComVisible(true)]
        public IDictionary<string, object> ToDictionary()
        {
            return GetType()
                .GetProperties(BindingFlags.Public)
                .Where(p => p.CanRead)
                .ToDictionary(
                    p => p.GetCustomAttribute<DataMemberAttribute>()?.Name ?? p.Name,
                    p =>
                    {
                        var value = p.GetValue(this);
                        return value is FieldsDictionary fieldsDictionary
                            ? fieldsDictionary.ToDictionary()
                            : value;
                    }
                );
        }
    }
}