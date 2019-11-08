using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Umbraco.Web;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager
{
    public class PhoneManagerService : IPhoneManagerService
    {

        private readonly IPublishedContentQuery _contentQuery;

        public PhoneManagerService(IPublishedContentQuery contentQuery)
        {
            _contentQuery = contentQuery;
        }

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
            var siteRoot = _contentQuery.ContentAtRoot().FirstOrDefault();
            var phoneManagerSection = (PhoneManagerModel)siteRoot?.FirstChild(f => f.ContentType.Alias == AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManager) ?? null;
            return phoneManagerSection;
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