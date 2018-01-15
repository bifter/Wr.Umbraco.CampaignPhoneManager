using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Umbraco.Web;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers.Storage.UmbracoContent
{
    /// <summary>
    /// NOT IN USE
    /// </summary>
    public class UmbracoContentRepository : IRepository
    {
        private CampaignPhoneManagerModel _defaultSettings;

        public CampaignDetail GetCampaignDetailById(string id)
        {
            throw new NotImplementedException();
        }

        public CampaignPhoneManagerModel GetDefaultSettings()
        {
            if (_defaultSettings == null)
                _defaultSettings = LoadDefaultSettings();

            return _defaultSettings;
        }

        private CampaignPhoneManagerModel LoadDefaultSettings()
        {
            var temp = UmbracoContext.Current.PublishedContentRequest.PublishedContent.Site().Descendant(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager);
            var result = Mapper.Map<Umbraco.Core.Models.IPublishedContent, CampaignPhoneManagerModel>(temp);
            return result;
        }

        public List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings)
        {
            throw new NotImplementedException();
        }

        public List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(string foundDefaultCampaignQSValue, string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings)
        {
            throw new NotImplementedException();
        }

        public List<CampaignDetail> GetMatchingCriteriaRecords_Referrer(string keyAlias, string referrer)
        {
            throw new NotImplementedException();
        }

        public List<CampaignDetail> GetMatchingRecords_Criteria_EntryPage(string keyAlias, string entrypage)
        {
            throw new NotImplementedException();
        }

        public List<CampaignDetail> ListAllCampaignDetailRecords()
        {
            throw new NotImplementedException();
        }

        public CampaignDetail GetDefaultCampaignDetail()
        {
            throw new NotImplementedException();
        }
    }
}