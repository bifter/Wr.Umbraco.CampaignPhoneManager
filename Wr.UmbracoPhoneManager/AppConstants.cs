namespace Wr.UmbracoPhoneManager
{
    public static class AppConstants
    {
        public const string LastResortPhoneNumberPlaceholder = "XXX XXX XXXX";

        public static class SessionKeys
        {
            public const string PM_Session = "umbPMSess";
        }

        public static class CookieKeys
        {
            public const string CookieMainKey = "umbPMCookie";
        }

        public static class UmbracoDocTypeAliases
        {
            public static class PhoneManagerModel
            {
                public const string PhoneManager = "phoneManager";
                public const string DefaultCampaignQueryStringKey = "defaultCampaignQueryStringKey";
                public const string DefaultPersistDurationInDays = "defaultPersistDurationInDays";
                public const string GlobalDisableOverwritePersistingItems = "globalDisableOverwritePersistingItems";
                public const string PhoneManagerCampaignDetail = "phoneManagerCampaignDetail";
            }

            public static class PhoneManagerModel_PhoneManagerCampaignDetail
            {
                public const string Id = "id";
                public const string NodeName = "nodeName";
                public const string DoNotPersistAcrossVisits = "doNotPersistAcrossVisits";
                public const string TelephoneNumber = "telephoneNumber";
                public const string PersistDurationOverride = "persistDurationOverride";
                public const string Referrer = "referrer";
                public const string CampaignCode = "campaignCode";
                public const string EntryPage = "entryPage";
                public const string OverwritePersistingItem = "overwritePersistingItem";
                public const string AltMarketingCode = "altMarketingCode";
                public const string PriorityOrder = "priorityOrder";
                public const string UseAltCampaignQueryStringKey = "useAltCampaignQueryStringKey";
                public const string IsDefault = "isDefault";
            }
        }

        public static class ConfigKeys
        {
            private const string AppName = "phoneManager.";

            public const string DiscoverNewCriteria = AppName + "DiscoverNewCriteria";
        }
    }
}