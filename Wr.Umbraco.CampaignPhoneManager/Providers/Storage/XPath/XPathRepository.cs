using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public partial class XPathRepository : XPathBaseSettings, IRepository
    {
        private readonly IXPathRepositoryImplementation _xpathImplementation;
        private CampaignPhoneManagerModel _defaultSettings;

        public XPathRepository()
        {
            _xpathImplementation = new IXPathRepositoryImplementation_UmbracoXPathNavigator(); // default xpath repository implementation
        }

        public XPathRepository(IXPathRepositoryImplementation xpathImplementation)
        {
            _xpathImplementation = xpathImplementation;
        }

        public CampaignPhoneManagerModel GetDefaultSettings()
        {
            if (_defaultSettings == null)
                _defaultSettings = _xpathImplementation.LoadDefaultSettings(xpath4DefaultCampaignPhoneManagerSettings);

            return _defaultSettings;
        }

        /// <summary>
        /// Matching QueryString records from storage
        /// </summary>
        /// <returns></returns>
        public List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(string foundDefaultCampaignQSValue, string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings)
        {
            // process campaignCodes with the default CampaignQuerystringKey, i.e. with no useAltCampaignQuerystringKey
            var selector = string.Format("(not({0}/text()[normalize-space()]) and {1}='{2}')", altCampaignQueryStringKeyAlias, campaignCodeAlias, foundDefaultCampaignQSValue);
            string xpath2 = string.Format(xpath4CampaignDetailHolder, selector);
            return _xpathImplementation.GetDataByXPath(xpath2); // add any matching records to the results
        }

        /// <summary>
        /// Matching QueryString records from storage using 'useAltCampaignQueryStringKey'
        /// </summary>
        /// <returns></returns>
        public List<CampaignDetail> GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(string campaignCodeAlias, string altCampaignQueryStringKeyAlias, NameValueCollection cleansedQueryStrings)
        {
            List<string> selectors = new List<string>();

            foreach (var key in cleansedQueryStrings) // loop around all the request querystrings
            {
                selectors.Add(string.Format("({0}='{2}' and {1}='{3}')", altCampaignQueryStringKeyAlias, campaignCodeAlias, key, cleansedQueryStrings[key.ToString()]));
            }
            string allSelectors = string.Join(" or ", selectors.ToArray()); // join all the selectors with 'or'
            string xpath = string.Format(xpath4CampaignDetailHolder, allSelectors);

            return _xpathImplementation.GetDataByXPath(xpath); // add any matching records to the results
        }

        /// <summary>
        /// Matching Referrer records from storage
        /// </summary>
        /// <returns></returns>
        public List<CampaignDetail> GetMatchingCriteriaRecords_Referrer(string referrerPropertyAlias, string referrer)
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            // look for records with the referrer property which match the end of the request referrer string
            // i.e. if the incoming request referrer is 'www.domain.co.uk', then a record with 'domain.co.uk' in the referrer field would match

            // 'ends-with' match. Note: XPath 1.0 doesn't implement 'ends-with' so we have to use substring workaround.
            var _selector = string.Format("boolean(substring('{0}', string-length('{0}') - string-length({1}/text()) +1) = {1}/text())", referrer, referrerPropertyAlias);

            // Basic match
            //var _selector = string.Format("{0}='{1}'", referrerPropertyAlias, referrer);

            string xpath = string.Format(xpath4CampaignDetailHolder, _selector);
            foundRecords.AddRange(_xpathImplementation.GetDataByXPath(xpath)); // add any matching records to the results

            return foundRecords;
        }

        /// <summary>
        /// Matching EntryPage records from storage
        /// </summary>
        /// <returns></returns>
        public List<CampaignDetail> GetMatchingRecords_Criteria_EntryPage(string entryPagePropertyAlias, string entrypage)
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            var _selector = string.Format("{0}='{1}'", entryPagePropertyAlias, entrypage);

            string xpath = string.Format(xpath4CampaignDetailHolder, _selector);
            foundRecords.AddRange(_xpathImplementation.GetDataByXPath(xpath)); // add any matching records to the results

            return foundRecords;
        }
    }
}