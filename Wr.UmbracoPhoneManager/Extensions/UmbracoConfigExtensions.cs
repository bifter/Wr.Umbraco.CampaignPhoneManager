using System.Threading;
using Umbraco.Core.Configuration;
using Wr.UmbracoPhoneManager.App_Config;

namespace Wr.UmbracoPhoneManager.Extensions
{
    public static class UmbracoConfigExtensions
    {
        private static AppSettingsConfig _config;

        /// <summary>
        /// Gets configuration for Phone Manager.
        /// </summary>
        /// <param name="umbracoConfig">The umbraco configuration.</param>
        /// <returns>The Phone Manager configuration.</returns>
        /// <remarks> 
        /// Getting the personalisation groups configuration freezes its state, and 
        /// any attempt at modifying the configuration will be ignored. 
        /// </remarks>
        public static AppSettingsConfig PhoneManager(this UmbracoConfig umbracoConfig)
        {
            LazyInitializer.EnsureInitialized(ref _config, () => AppSettingsConfig.Value);
            return _config;
        }

        // Internal for tests
        internal static void ResetConfig()
        {
            _config = null;
        }
    }
}