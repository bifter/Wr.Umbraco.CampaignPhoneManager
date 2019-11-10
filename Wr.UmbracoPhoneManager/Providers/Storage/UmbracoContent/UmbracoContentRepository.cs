using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Umbraco.Core.Mapping;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers.Storage.UmbracoContent
{
    /// <summary>
    /// NOT IN USE
    /// </summary>
    public class UmbracoContentRepository : IRepository
    {
        private PhoneManagerModel _defaultSettings;

        public PhoneManagerCampaignDetail GetCampaignDetailById(string id)
        {
            throw new NotImplementedException();
        }

        public PhoneManagerModel GetDefaultSettings()
        {
            if (_defaultSettings == null)
                _defaultSettings = LoadDefaultSettings();

            return _defaultSettings;
        }

        private PhoneManagerModel LoadDefaultSettings()
        {
            var phoneManagerIPub = Umbraco.Web.Composing.Current.UmbracoContext.PublishedRequest.PublishedContent.Root().Descendant(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManager);
            return MapIPublishedContentToPhoneManagerModel(phoneManagerIPub);
        }

        private PhoneManagerModel MapIPublishedContentToPhoneManagerModel(IPublishedContent model)
        {
            PhoneManagerModel result = new PhoneManagerModel();
            result.DefaultCampaignQueryStringKey = model.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.DefaultCampaignQueryStringKey).ToString();
            result.DefaultPersistDurationInDays = Convert.ToInt32(model.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.DefaultPersistDurationInDays));
            result.GlobalDisableOverwritePersistingItems = Convert.ToBoolean(model.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.GlobalDisableOverwritePersistingItems));

            var campaignDetailItems = model.Value<IEnumerable<IPublishedContent>>(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManagerCampaignDetail);
            foreach(var item in campaignDetailItems)
            {
                PhoneManagerCampaignDetail newCD = new PhoneManagerCampaignDetail();
                newCD.Id = item.Id.ToString();
                newCD.NodeName = item.Name;
                newCD.Referrer = item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.Referrer).ToString();
                newCD.AltMarketingCode = item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.AltMarketingCode).ToString();
                newCD.CampaignCode = item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.CampaignCode).ToString();
                newCD.DoNotPersistAcrossVisits = Convert.ToBoolean(item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.DoNotPersistAcrossVisits));
                newCD.EntryPage = item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.EntryPage).ToString();
                newCD.IsDefault = Convert.ToBoolean(item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.IsDefault));
                newCD.OverwritePersistingItem = Convert.ToBoolean(item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.OverwritePersistingItem));
                newCD.PersistDurationOverride = Convert.ToInt32(item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.PersistDurationOverride));
                newCD.PriorityOrder = Convert.ToInt32(item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.PriorityOrder));
                newCD.TelephoneNumber = item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.TelephoneNumber).ToString();
                newCD.UseAltCampaignQueryStringKey = item.Value(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.UseAltCampaignQueryStringKey).ToString();
                result.PhoneManagerCampaignDetail.Add(newCD);
            }

            return result;

        }


        public List<PhoneManagerCampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings)
        {
            throw new NotImplementedException();
        }

        public List<PhoneManagerCampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(string foundDefaultCampaignQSValue, string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings)
        {
            throw new NotImplementedException();
        }

        public List<PhoneManagerCampaignDetail> GetMatchingCriteriaRecords_Referrer(string keyAlias, string referrer)
        {
            throw new NotImplementedException();
        }

        public List<PhoneManagerCampaignDetail> GetMatchingRecords_Criteria_EntryPage(string keyAlias, string entrypage)
        {
            throw new NotImplementedException();
        }

        public List<PhoneManagerCampaignDetail> ListAllCampaignDetailRecords()
        {
            return _defaultSettings.PhoneManagerCampaignDetail.ToList();
        }

        public PhoneManagerCampaignDetail GetDefaultCampaignDetail()
        {
            PhoneManagerCampaignDetail result = new PhoneManagerCampaignDetail();
            return _defaultSettings.PhoneManagerCampaignDetail.Where(x => x.IsDefault).FirstOrDefault();
        }
    }
}