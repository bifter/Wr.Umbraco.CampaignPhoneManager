using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public partial class XPathRepository : XPathBase, IRepository
    {
        private readonly IXPathRepositoryImplementation _xpathImplementation;
        private CampaignPhoneManagerModel _defaultSettings;

        public XPathRepository()
        {
            _xpathImplementation = new IXPathRepositoryImplementation_UmbracoXPathNavigator(); // default implementation
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
        public List<CampaignDetail> GetMatchingCriteriaRecords_QueryString(NameValueCollection cleansedQueryStrings)
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            TODO Move statics to Criteria methods

            // first process campaignCodes with the default CampaignQuerystringKey, i.e. with no useAltCampaignQuerystringKey
            var defaultQSValue = cleansedQueryStrings[GetDefaultSettings().DefaultCampaignQueryStringKey] ?? string.Empty;
            if (!string.IsNullOrEmpty(defaultQSValue)) // default campaign qs key/value found
            {
                var selector = string.Format("not({0}/text()[normalize-space()]) and {1}='{2}'", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode, defaultQSValue);
                string xpath2 = string.Format(xpath4CampaignDetailHolder, selector);
                foundRecords.AddRange(_xpathImplementation.GetDataByXPath(xpath2)); // add any matching records to the results
            }

            // now find records with an useAltCampaignQuerystringKey
            List<string> selectors = new List<string>();

            foreach (var key in cleansedQueryStrings) // loop around all the request querystrings
            {
                selectors.Add(string.Format("({0}='{2}' and {1}='{3}')", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode, key, cleansedQueryStrings[key.ToString()]));
            }
            string allSelectors = string.Join(" or ", selectors.ToArray()); // join all the selectors with 'or'
            string xpath = string.Format(xpath4CampaignDetailHolder, allSelectors);
            foundRecords.AddRange(_xpathImplementation.GetDataByXPath(xpath)); // add any matching records to the results

            return foundRecords;
        }

        /// <summary>
        /// Matching Referrer records from storage
        /// </summary>
        /// <returns></returns>
        public List<CampaignDetail> GetMatchingCriteriaRecords_Referrer(string referrerPropertyAlias, string referrer)
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            var _selector = string.Format("{0}='{1}'", referrerPropertyAlias, referrer);

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