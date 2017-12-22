using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class QueryStringCriteria_DataSource_XPath : XPathDataProviderBase, IXPathCriteriaDataProvider
    {
        private readonly NameValueCollection _cleansedQueryStrings;
        private readonly IXPathDataProviderSource _dataSource;

        public QueryStringCriteria_DataSource_XPath(CriteriaParameterHolder criteriaParameters)
        {
            _cleansedQueryStrings = criteriaParameters.CleansedQueryStrings;
            _dataSource = new XPathDataProviderSource_UmbracoGetXPathNavigator();
        }

        public QueryStringCriteria_DataSource_XPath(CriteriaParameterHolder criteriaParameters, IXPathDataProviderSource dataSource)
        {
            _cleansedQueryStrings = criteriaParameters.CleansedQueryStrings;
            _dataSource = dataSource;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            // first process campaignCodes with the default CampaignQuerystringKey, i.e. with no useAltCampaignQuerystringKey
            var defaultQSValue = _cleansedQueryStrings[_dataSource.GetDefaultSettings().DefaultCampaignQueryStringKey] ?? string.Empty;
            if (!string.IsNullOrEmpty(defaultQSValue)) // default campaign qs key/value found
            {
                var selector = string.Format("not({0}/text()[normalize-space()]) and {1}='{2}'", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode, defaultQSValue);
                string xpath2 = string.Format(xpath4CampaignDetailHolder, selector);
                foundRecords.AddRange(_dataSource.GetDataByXPath(xpath2)); // add any matching records to the results
            }

            // now find records with an useAltCampaignQuerystringKey
            List<string> selectors = new List<string>();

            foreach (var key in _cleansedQueryStrings) // loop around all the request querystrings
            {
                selectors.Add(string.Format("({0} = '{2}' and {1} = '{3}')", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode, key, _cleansedQueryStrings[key.ToString()]));
            }
            string allSelectors = string.Join(" or ", selectors.ToArray()); // join all the selectors with 'or'
            string xpath = string.Format(xpath4CampaignDetailHolder, allSelectors);
            foundRecords.AddRange(_dataSource.GetDataByXPath(xpath)); // add any matching records to the results
            
            return foundRecords;
        }
    }
}