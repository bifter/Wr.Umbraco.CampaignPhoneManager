using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Wr.UmbracoPhoneManager.Install
{
    public static class Setup
    {
        public static bool DoAllDocTypesExist()
        {
            var cts = ApplicationContext.Current.Services.ContentTypeService;
            var foundCampaignDetailContentType = cts.GetContentType(DocTypeAliases.PhoneManagerCampaignDetail);
            var foundCampaignPhoneManagerContentType = cts.GetContentType(DocTypeAliases.PhoneManager);

            if (foundCampaignDetailContentType != null && foundCampaignPhoneManagerContentType != null)
            {
                return true;
            }
            return false;
        }

        public static void CreateDocTypes()
        {
            var cts = ApplicationContext.Current.Services.ContentTypeService;

            // CampaignDetail
            var foundContentType = cts.GetContentType(DocTypeAliases.PhoneManagerCampaignDetail);
            if (foundContentType == null)
            {
                // create doctype
                ContentType ctModel = new ContentType(-1);
                ctModel.Name = "Campaign Detail";
                ctModel.Alias = DocTypeAliases.PhoneManagerCampaignDetail;
                ctModel.Icon = "icon-phone";
                var tabName = "Campaign Detail";
                ctModel.AddPropertyGroup(tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TextString) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.TelephoneNumber, Name = "Telephone number", Description = "The campaign phone number", Mandatory = true, SortOrder = 1 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TextString) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.CampaignCode, Name = "Campaign code", Description = "The campaign code sent as a querystring 'fsource=' with the request", Mandatory = false, SortOrder = 2 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TextString) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.Referrer, Name = "Referrer", Description = "With the following referrer domains. E.g. with could be search engine domains. Use 'none' for direct entry.", Mandatory = false, SortOrder = 3 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.ContentPicker2) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.EntryPage, Name = "Entry page", Description = "If user enters the site on this page", Mandatory = false, SortOrder = 4 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TrueFalse) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.DoNotPersistAcrossVisits, Name = "Do not persist across visits", Description = "Persist this number across user sessions. i.e. using cookies", Mandatory = false, SortOrder = 5 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.Numeric) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.PersistDurationOverride, Name = "Persist duration override", Description = "If persisting, for how many days?", Mandatory = true, SortOrder = 6 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TrueFalse) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.OverwritePersistingItem, Name = "Overwrite persisting item", Description = "This number will overwrite an exisiting persistent number", Mandatory = false, SortOrder = 7 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TextString) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.AltMarketingCode, Name = "Alt marketing code", Description = "Additional marketing code associated with this campaign", Mandatory = false, SortOrder = 8 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TextString) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.UseAltCampaignQueryStringKey, Name = "Override campaign querystring key", Description = "Use this as the QueryString key instead of the default one.", Mandatory = false, SortOrder = 9 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TrueFalse) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.IsDefault, Name = "Is Default", Description = "Use this if no other matching campaigns are found", Mandatory = false, SortOrder = 10 }, tabName);

                cts.Save(ctModel);
            }

            // CampaignPhoneManagerModel
            foundContentType = cts.GetContentType(DocTypeAliases.PhoneManager);
            if (foundContentType == null)
            {
                // create doctype
                ContentType ctModel = new ContentType(-1);
                ctModel.Name = "Phone Manager";
                ctModel.Alias = DocTypeAliases.PhoneManager;
                ctModel.Icon = "icon-phone-ring color-orange";
                var tabName = "Settings";
                ctModel.AddPropertyGroup(tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TextString) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.DefaultCampaignQueryStringKey, Name = "Campaign querystring key", Description = "The http request querystring key used by the marketing campaign", Mandatory = true, SortOrder = 1}, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.Numeric) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.DefaultPersistDurationInDays, Name = "Default persist duration in days", Description = "The default number of days to persist the campaign for a customer", Mandatory = true, SortOrder = 3 }, tabName);

                ctModel.AddPropertyType(new PropertyType(UmbracoDataTypes.TrueFalse) { Alias = AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.GlobalDisableOverwritePersistingItems, Name = "Disable any campaign from overwriting persisting items. This is recommended if possible.", Mandatory = false, SortOrder = 4 }, tabName);

                // Add the CampaignDetail content type as allowed under this
                var foundCampaignDetailType = cts.GetContentType(DocTypeAliases.PhoneManagerCampaignDetail);
                ctModel.AllowedContentTypes = new List<ContentTypeSort>(foundCampaignDetailType.Id);

                cts.Save(ctModel);
            }
        }

        #region >>>> DocType helpers

        private static class UmbracoDataTypes
        {
            public static DataTypeDefinition ContentPicker2 = new DataTypeDefinition(-1, "Umbraco.ContentPicker2");
            public static DataTypeDefinition Numeric = new DataTypeDefinition(-1, "Umbraco.Numeric");
            public static DataTypeDefinition TextString = new DataTypeDefinition(-1, "Umbraco.Textbox");
            public static DataTypeDefinition TrueFalse = new DataTypeDefinition(-1, "Umbraco.TrueFalse");
        }

        private static class DocTypeAliases
        {
            public static string PhoneManagerCampaignDetail = "testPhoneManagerCampaignDetail"; // AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.CampaignDetail
            public static string PhoneManager = "testPhoneManager"; // AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.CampaignPhoneManager
        }
        #endregion
    }
}