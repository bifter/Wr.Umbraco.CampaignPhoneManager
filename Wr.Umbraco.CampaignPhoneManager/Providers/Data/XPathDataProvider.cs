using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{

    public class XPathDataProvider : IDataProvider
    {
        private DefaultSettings _defaultSettings { get; set; }
        private IXPathDataProviderSource _xPathDataProviderSource { get; set; }

        public XPathDataProvider(IXPathDataProviderSource xPathDataProviderSource)
        {
            _xPathDataProviderSource = xPathDataProviderSource;
        }

        public DefaultSettings GetDefaultSettings()
        {
            if (_defaultSettings == null)
            {
                _defaultSettings = LoadDefaultSettings();
            }
            return _defaultSettings;
        }

        private DefaultSettings LoadDefaultSettings()
        {
            return _xPathDataProviderSource.LoadDefaultSettings("$ancestorOrSelf/ancestor-or-self::home[position()=1]//campaignPhoneManager");
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
            var xpathBase = "$ancestorOrSelf/ancestor-or-self::home[position()=1]//campaignPhoneManager/campaignDetail[{0}]";

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
                    var selector = string.Format("useAltCampaignQueryStringKey='' and campaignCode='{0}'", defaultQSValue);
                    string xpath2 = string.Format(xpathBase, selector);
                    foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath2)); // add any matching records to the results
                }

                // now find records with an useAltCampaignQuerystringKey
                List<string> selectors = new List<string>();
                
                foreach (var key in cleansedQuerystrings) // loop around all the request querystrings
                {
                    selectors.Add(string.Format("(useAltCampaignQueryStringKey = '{0}' and campaignCode = '{1}')", key, cleansedQuerystrings[key.ToString()]));
                }
                string allSelectors = string.Join(" or ", selectors.ToArray()); // join all the selectors with 'or'
                string xpath = string.Format(xpathBase, allSelectors);
                foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // referrer
            if (!string.IsNullOrEmpty(requestInfoNotIncludingQueryStrings.referrer))
            {
                referrerSelector = string.Format("(referrer='{0}')", requestInfoNotIncludingQueryStrings.referrer);

                string xpath = string.Format(xpathBase, referrerSelector);
                foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // entry page
            if (!string.IsNullOrEmpty(requestInfoNotIncludingQueryStrings.entryPage))
            {
                entryPageSelector = string.Format("entryPage='{0}'", requestInfoNotIncludingQueryStrings.entryPage);

                string xpath = string.Format(xpathBase, entryPageSelector);
                foundRecords.AddRange(_xPathDataProviderSource.GetDataByXPath(xpath)); // add any matching records to the results
            }


            // work out which record to use
            // - group identical records, then order by group member count. We will want to use the first grouped item, as this will have the most matching criteria
            var priorityList = (from rec in foundRecords
                                group rec by rec into gr // group duplicate records
                                orderby gr.Count() descending // order by the group item count. The most number of duplicate records, the higher the priority, so we will want the first record in the list
                                orderby gr.First().priorityOrder descending // then order by the priority of a member of the group. 'First' is chosen for convenience and also as all groups will have at least one member
                                select gr.First()
                                ).FirstOrDefault(); // get the first item in each group

            return (priorityList != null) ? priorityList : new CampaignDetail();
        }
    }
}