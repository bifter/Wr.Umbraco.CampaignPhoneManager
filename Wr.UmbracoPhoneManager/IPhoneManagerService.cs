using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager
{
    public interface IPhoneManagerService
    {
        PhoneManagerModel GetDefaultSettings();

        PhoneManagerCampaignDetail GetDefaultCampaignDetail(); // used when no matching criteria records found

        PhoneManagerCampaignDetail GetCampaignDetailById(string id);

        List<PhoneManagerCampaignDetail> ListAllCampaignDetailRecords();

        List<PhoneManagerCampaignDetail> GetMatchingRecords_Criteria_EntryPage(string keyAlias, string entrypage);

        List<PhoneManagerCampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(string foundDefaultCampaignQSValue, string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings);

        List<PhoneManagerCampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings);

        List<PhoneManagerCampaignDetail> GetMatchingCriteriaRecords_Referrer(string keyAlias, string referrer);
    }
}