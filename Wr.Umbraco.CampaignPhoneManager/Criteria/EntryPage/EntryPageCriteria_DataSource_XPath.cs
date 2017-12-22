using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class EntryPageCriteria_DataSource_XPath : XPathDataProviderBase, IXPathCriteriaDataSource
    {
        private readonly string _entryPage;
        private readonly IXPathDataProviderSource _dataSource;

        public EntryPageCriteria_DataSource_XPath(CriteriaParameterHolder criteriaParameters)
        {
            _entryPage = criteriaParameters.RequestInfoNotIncludingQueryStrings.EntryPage;
            _dataSource = new XPathDataProviderSource_UmbracoGetXPathNavigator();
        }

        public EntryPageCriteria_DataSource_XPath(CriteriaParameterHolder criteriaParameters, IXPathDataProviderSource dataSource)
        {
            _entryPage = criteriaParameters.RequestInfoNotIncludingQueryStrings.EntryPage;
            _dataSource = dataSource;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            var entryPageSelector = string.Format("{0}='{1}'", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage, _entryPage);

            string xpath = string.Format(xpath4CampaignDetailHolder, entryPageSelector);
            foundRecords.AddRange(_dataSource.GetDataByXPath(xpath)); // add any matching records to the results

            return foundRecords;
        }
    }
}