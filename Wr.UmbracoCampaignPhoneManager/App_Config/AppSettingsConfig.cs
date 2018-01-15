using System.Configuration;

namespace Wr.UmbracoCampaignPhoneManager.App_Config
{
    public class AppSettingsConfig
    {
        private static AppSettingsConfig _value;

        /// <summary>
        /// Setup AppSettings for the app
        /// with details from configuration.
        /// </summary>
        private AppSettingsConfig()
        {
            DiscoverNewCriteria = GetConfigBoolValue(AppConstants.ConfigKeys.DiscoverNewCriteria, false);
        }

        private static bool GetConfigBoolValue(string key, bool defaultValue)
        {
            return bool.TryParse(GetConfigStringValue(key), out bool value) ? value : defaultValue;
        }

        private static int GetConfigIntValue(string key, int defaultValue)
        {
            return int.TryParse(GetConfigStringValue(key), out int value) ? value : defaultValue;
        }

        private static string GetConfigStringValue(string key, string defaultValue = "")
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        /// <summary>
        /// Initializes a new instance with details passed as parameters.
        /// </summary>
        public AppSettingsConfig(bool discoverNewCriteria = false)
        {
            DiscoverNewCriteria = discoverNewCriteria;
        }

        internal static AppSettingsConfig Value => _value ?? new AppSettingsConfig();

        public static void Setup(AppSettingsConfig config)
        {
            _value = config;
        }

        public bool DiscoverNewCriteria { get; }

    }
}