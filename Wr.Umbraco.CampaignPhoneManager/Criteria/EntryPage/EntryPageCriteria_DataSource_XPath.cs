using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria.EntryPage
{
    public class EntryPageCriteria_DataSource_XPath : XPathDataProviderBase, IDataProvider
    {
        private readonly string _entryPage;

        public EntryPageCriteria_DataSource_XPath(string entryPage)
        {
            _entryPage = entryPage;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            var entryPageSelector = string.Format("{0}='{1}'", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage, _entryPage);

            string xpath = string.Format(xpathHolder, entryPageSelector);
            foundRecords.AddRange(GetDataByXPath(xpath)); // add any matching records to the results

            return foundRecords;
        }
    }
}