using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
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
            var temp = UmbracoContext.Current.PublishedContentRequest.PublishedContent.Site().Descendant(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManager);
            var result = Mapper.Map<Umbraco.Core.Models.IPublishedContent, PhoneManagerModel>(temp);
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
            throw new NotImplementedException();
        }

        public PhoneManagerCampaignDetail GetDefaultCampaignDetail()
        {
            throw new NotImplementedException();
        }
    }
}