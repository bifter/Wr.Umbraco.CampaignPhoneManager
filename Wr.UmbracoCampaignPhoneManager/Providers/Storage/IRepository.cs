using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers.Storage
{
    public partial interface IRepository
    {
        CampaignPhoneManagerModel GetDefaultSettings();

        CampaignDetail GetCampaignDetailById(string id);

        List<CampaignDetail> GetMatchingRecords_Criteria_EntryPage(string keyAlias, string entrypage);

        List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(string foundDefaultCampaignQSValue, string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings);

        List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings);

        List<CampaignDetail> GetMatchingCriteriaRecords_Referrer(string keyAlias, string referrer);
    }
}