using System.Configuration;
using System.Reflection;
using NUnit.Framework;

namespace Qiwi.BillPayments.Tests
{
    public static class Config
    {
        private static KeyValueConfigurationCollection Configuration { get; }
            = loadConfiguration(Assembly.GetExecutingAssembly());
        
        public static string MerchantPublicKey
            => Configuration?["MerchantPublicKey"]?.Value ?? "Fnzr1yTebUiQaBLDnebLMMxL8nc6FF5zfmGQnypc*******";
        
        public static string MerchantSecretKey
            => Configuration?["MerchantSecretKey"]?.Value ?? "MjMyNDQxMjM6NDUzRmRnZDQ0M*******";
        
        public static string BillIdForRefundTest
            => Configuration?["BillIdForRefundTest"]?.Value ?? "899343443";
        
        public static void Required()
        {
            if (!ConfigIsLoaded())
            {
                Assert.Ignore("Test required config");
            }
        }
        
        private static KeyValueConfigurationCollection loadConfiguration(Assembly assembly)
        {
            try
            {
                return ConfigurationManager
                    .OpenExeConfiguration(assembly.Location)
                    .AppSettings
                    .Settings;
            }
            catch
            {
                return null;
            }
        }
        
        private static bool ConfigIsLoaded()
        {
            return Configuration?["MerchantPublicKey"]?.Value != null &&
                   Configuration?["MerchantSecretKey"]?.Value != null &&
                   Configuration?["BillIdForRefundTest"]?.Value != null;
        }
    }
}
