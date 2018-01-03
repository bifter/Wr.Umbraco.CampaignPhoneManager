using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public partial interface IRepository
    {
        CampaignPhoneManagerModel GetDefaultSettings();

        List<CampaignDetail> GetMatchingRecords_Criteria_EntryPage(string keyAlias, string entrypage);

        List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(string foundDefaultCampaignQSValue, string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings);

        List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings);

        List<CampaignDetail> GetMatchingCriteriaRecords_Referrer(string keyAlias, string referrer);
    }
}