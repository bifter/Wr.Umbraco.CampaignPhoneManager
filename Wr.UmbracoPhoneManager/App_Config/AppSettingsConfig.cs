using System.Configuration;

namespace Wr.UmbracoPhoneManager.App_Config
{
    /// <summary>
    /// Adapted from Personalisation Groups https://github.com/AndyButland/UmbracoPersonalisationGroups
    /// </summary>
    public class AppSettingsConfig
    {
        private static AppSettingsConfig _value;

        public bool DiscoverNewCriteria { get; }
        public bool EnablePhoneManagerInRoot { get; }

        /// <summary>
        /// Setup AppSettings for the app
        /// with details from configuration.
        /// </summary>
        private AppSettingsConfig()
        {
            DiscoverNewCriteria = GetConfigBoolValue(AppConstants.ConfigKeys.DiscoverNewCriteria, false);
            EnablePhoneManagerInRoot = GetConfigBoolValue(AppConstants.ConfigKeys.EnablePhoneManagerInRoot, false);
        }

        /// <summary>
        /// Initializes a new instance with details passed as parameters.
        /// </summary>
        public AppSettingsConfig(bool discoverNewCriteria = false, bool enablePhoneManagerInRoot = false)
        {
            DiscoverNewCriteria = discoverNewCriteria;
            EnablePhoneManagerInRoot = enablePhoneManagerInRoot;
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

        internal static AppSettingsConfig Value => _value ?? new AppSettingsConfig();

        public static void Setup(AppSettingsConfig config)
        {
            _value = config;
        }
    }
}