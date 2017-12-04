using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Umbraco.Core.Models;
using Umbraco.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{

    public class XPathDataProvider : IDataProvider
    {
        private DefaultSettings _defaultSettings { get; set; }
        private IXPathDataSource _xPathDataSource { get; set; }

        public XPathDataProvider(IXPathDataSource xPathDataSource)
        {
            _xPathDataSource = xPathDataSource;
            _defaultSettings = LoadDefaultSettings();
        }

        public DefaultSettings GetDefaultSettings()
        {
            return _defaultSettings;
        }

        private DefaultSettings LoadDefaultSettings()
        {
            return _xPathDataSource.LoadDefaultSettings("$ancestorOrSelf/ancestor-or-self::home[position()=1]//phoneManager");
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
        public PhoneNumber GetMatchingRecordFromPhoneManager(PhoneNumber requestInfo, NameValueCollection cleansedQuerystrings, string testXmlForUnitTesting = "")
        {
            bool forTesting = !string.IsNullOrEmpty(testXmlForUnitTesting);

            // build up xpath query
            var xpathBase = "$ancestorOrSelf/ancestor-or-self::home[position()=1]//phoneManager/phoneNumber[{0}]";

            string referrerSelector = string.Empty;
            string entryPageSelector = string.Empty;

            List<PhoneNumber> foundRecords = new List<PhoneNumber>();

            // querystrings
            // - note: take into consideration the useAltCampaignQuerystringKey property in the records

            if (cleansedQuerystrings.Count > 0)
            {
                // first process campaignCodes with the default CampaignQuerystringKey, i.e. with no useAltCampaignQuerystringKey
                var defaultQSValue = cleansedQuerystrings[_defaultSettings.DefaultCampaignQuerystringKey] ?? string.Empty;
                if (!string.IsNullOrEmpty(defaultQSValue)) // default campaign qs key/value found
                {
                    var selector = string.Format("useAltCampaignQuerystringKey='' and campaignCode='{0}'", defaultQSValue);
                    string xpath2 = string.Format(xpathBase, selector);
                    foundRecords.AddRange(_xPathDataSource.GetDataByXPath(xpath2)); // add any matching records to the results
                }

                // now find records with an useAltCampaignQuerystringKey
                List<string> selectors = new List<string>();
                
                foreach (var key in cleansedQuerystrings) // loop around all the request querystrings
                {
                    selectors.Add(string.Format("(useAltCampaignQuerystringKey = '{0}' and campaignCode = '{1}')", key, cleansedQuerystrings[key.ToString()]));
                }
                string allSelectors = string.Join(" or ", selectors.ToArray()); // join all the selectors with 'or'
                string xpath = string.Format(xpathBase, allSelectors);
                foundRecords.AddRange(_xPathDataSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // referrer
            if (!string.IsNullOrEmpty(requestInfo.referrer))
            {
                referrerSelector = string.Format("(referrer='{0}')", requestInfo.referrer);

                string xpath = string.Format(xpathBase, referrerSelector);
                foundRecords.AddRange(_xPathDataSource.GetDataByXPath(xpath)); // add any matching records to the results
            }

            // entry page
            if (!string.IsNullOrEmpty(requestInfo.entryPage))
            {
                entryPageSelector = string.Format("entryPage='{0}'", requestInfo.entryPage);

                string xpath = string.Format(xpathBase, entryPageSelector);
                foundRecords.AddRange(_xPathDataSource.GetDataByXPath(xpath)); // add any matching records to the results
            }


            // work out which record to use
            // - group identical records, then order by group member count. We will want to use the first grouped item, as this will have the most matching criteria
            var priorityList = (from rec in foundRecords
                                group rec by rec into gr // group duplicate records
                                orderby gr.Count() descending // order by the group item count. The most number of duplicate records, the higher the priority, so we will want the first record in the list
                                orderby gr.First().priorityOrder descending // then order by the priority of a member of the group. 'First' is chosen for convenience and also as all groups will have at least one member
                                select gr.First()
                                ).FirstOrDefault(); // get the first item in each group

            return null;
        }

    }
}