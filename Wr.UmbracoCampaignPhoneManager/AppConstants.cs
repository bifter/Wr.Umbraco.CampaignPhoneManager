namespace Wr.UmbracoCampaignPhoneManager
{
    public static class AppConstants
    {
        public const string LastResortPhoneNumberPlaceholder = "XXX XXX XXXX";

        public static class SessionKeys
        {
            public const string PM_Session = "umbCPMSess";
        }

        public static class CookieKeys
        {
            public const string CookieMainKey = "umbCPMCookie";
            public const string SubKey_Id = "umbCPM_ID";
            public const string SubKey_TelephoneNumber = "umbCPM_TN";
            public const string SubKey_CampaignCode = "umbCPM_CC";
            public const string SubKey_AltMarketingCode = "umbCPM_AMC";
        }
        
        public static class UmbracoDocTypeAliases
        {
            public static class CampaignPhoneManagerModel
            {
                public const string CampaignPhoneManager = "campaignPhoneManager";
                public const string DefaultCampaignQueryStringKey = "defaultCampaignQueryStringKey";
                public const string DefaultPhoneNumber = "defaultPhoneNumber";
                public const string DefaultPersistDurationInDays = "defaultPersistDurationInDays";
                public const string GlobalDisableOverwritePersistingItems = "globalDisableOverwritePersistingItems";
                public const string CampaignDetail = "campaignDetail";
            }

            public static class CampaignPhoneManagerModel_CampaignDetail
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
            }
        }
    }
}