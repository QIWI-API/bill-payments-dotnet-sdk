using System.Linq;
using System.Runtime.InteropServices;

namespace Qiwi.BillPayments.Utils
{
    /// <summary>
    /// Universal object factory.
    /// </summary>
    /// <typeparam name="TI">The object interface.</typeparam>
    /// <typeparam name="TD">The default object type.</typeparam>
    [ComVisible(true)]
    public abstract class AbstractFactory<TI, TD> where TD : class, TI
    {
        /// <summary>
        /// Build new object instance.
        /// </summary>
        /// <param name="args">The constructor arguments.</param>
        /// <returns>The object.</returns>
        [ComVisible(true)]
        public static TD create(params object[] args)
        {
            return create<TD>(args);
        }
        
        /// <summary>
        /// Build new object instance.
        /// </summary>
        /// <param name="args">The constructor arguments.</param>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object.</returns>
        [ComVisible(true)]
        public static T create<T>(params object[] args) where T : class, TI
        {
            try
            {
                return (T)typeof(T).GetConstructor(args.Select(o => o.GetType()).ToArray())?.Invoke(args);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
