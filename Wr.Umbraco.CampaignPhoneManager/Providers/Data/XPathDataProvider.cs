using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Logic for all XPathDataProvider Sources
    /// </summary>
    public class XPathDataProvider : IDataProvider
    {
        private CampaignPhoneManagerModel _defaultSettings { get; set; }
        private IXPathDataProviderSource _xPathDataProviderSource { get; set; }
        private const string baseXpath = "$ancestorOrSelf/ancestor-or-self::home[position()=1]//";

        public XPathDataProvider(IXPathDataProviderSource xPathDataProviderSource)
        {
            _xPathDataProviderSource = xPathDataProviderSource;
        }

        public CampaignPhoneManagerModel GetDefaultSettings()
        {
            if (_defaultSettings == null)
            {
                _defaultSettings = LoadDefaultSettings();
            }
            return _defaultSettings;
        }

        private CampaignPhoneManagerModel LoadDefaultSettings()
        {
            return _xPathDataProviderSource.LoadDefaultSettings(string.Format("{0}{1}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager));
        }

        /// <summary>
        /// Get matching phone number, based on
        ///  - querystring(s)
        ///  - referrer
        ///  - entry page
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="cleansedQuerystrings"></param>
        /// <param name="testXmlForUnitTesting">xml test data for Unit Testing</param>
        /// <returns>Found PhoneNumber</returns>
        public CampaignDetail GetMatchingRecordFromPhoneManager(CampaignDetail requestInfoNotIncludingQueryStrings, NameValueCollection cleansedQuerystrings)
        {
            // build up xpath query
            var xpathHolder = string.Format("{0}{1}/{2}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail) + "[{0}]";
            string referrerSelector = string.Empty;
            string entryPageSelector = string.Empty;

            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            // querystrings
            // - note: take into consideration the useAltCampaignQuerystringKey property in the records

            if (cleansedQuerystrings.Count > 0)
            {
                // first process campaignCodes with the default CampaignQuerystringKey, i.e. with no useAltCampaignQuerystringKey
                var defaultQSValue = cleansedQuerystrings[GetDefaultSettings().DefaultCampaignQueryStringKey] ?? string.Empty;
                if (!string.IsNullOrEmpty(defaultQSValue)) // default campaign qs key/value found
                {
                    var selector = string.Format("not({0}/text()[normalize-space()]) and {1}='{2}'", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode, defaultQSValue);
                    string xpath2 = string.Format(xpathHolder, selector);
                    foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath2)); // add any matching records to the results
                }

                // now find records with an useAltCampaignQuerystringKey
                List<string> selectors = new List<string>();
                
                foreach (var key in cleansedQuerystrings) // loop around all the request querystrings
                {
                    selectors.Add(string.Format("({0} = '{2}' and {1} = '{3}')", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode, key, cleansedQuerystrings[key.ToString()]));
                }
                string allSelectors = string.Join(" or ", selectors.ToArray()); // join all the selectors with 'or'
                string xpath = string.Format(xpathHolder, allSelectors);
                foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // referrer
            if (!string.IsNullOrEmpty(requestInfoNotIncludingQueryStrings.Referrer))
            {
                referrerSelector = string.Format("contains({0}, '{1}')", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.Referrer, requestInfoNotIncludingQueryStrings.Referrer.Replace("www.", ""));

                string xpath = string.Format(xpathHolder, referrerSelector);
                foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // entry page
            if (!string.IsNullOrEmpty(requestInfoNotIncludingQueryStrings.EntryPage))
            {
                entryPageSelector = string.Format("{0}='{1}'", AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage, requestInfoNotIncludingQueryStrings.EntryPage);

                string xpath = string.Format(xpathHolder, entryPageSelector);
                foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // work out which record to use
            // - group identical records (using the Id property), then order by group member count. We will want to use the first grouped item, as this will have the most matching criteria
            var priorityList = (from rec in foundRecords
                                group rec by rec.Id into gr // group duplicate records
                                orderby gr.Count() descending // order by the group item count. The most number of duplicate records, the higher the priority, so we will want the first record in the list
                                orderby gr.First().PriorityOrder descending // then order by the priority of a member of the group. 'First' is chosen for convenience and also as all groups will have at least one member
                                select gr.First() // get the first item in each group
                                ).FirstOrDefault(); // return the first group

            return priorityList;
        }
    }
}