using System.Threading;
using Umbraco.Core.Configuration;
using Wr.UmbracoCampaignPhoneManager.App_Config;

namespace Wr.UmbracoCampaignPhoneManager.Extensions
{
    public static class UmbracoConfigExtensions
    {
        private static AppSettingsConfig _config;

        /// <summary>
        /// Gets configuration for Campaign Phone Manager.
        /// </summary>
        /// <param name="umbracoConfig">The umbraco configuration.</param>
        /// <returns>The Campaign Phone Manager configuration.</returns>
        /// <remarks> 
        /// Getting the personalisation groups configuration freezes its state, and 
        /// any attempt at modifying the configuration will be ignored. 
        /// </remarks>
        public static AppSettingsConfig CampaignPhoneManager(this UmbracoConfig umbracoConfig)
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