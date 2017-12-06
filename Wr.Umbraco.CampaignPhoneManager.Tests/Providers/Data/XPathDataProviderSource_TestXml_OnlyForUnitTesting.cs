using System;
using System.Collections.Generic;
using System.Xml;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers
{
    public class XPathDataProviderSource_TestXml_OnlyForUnitTesting : IXPathDataProviderSource
    {

        private string _testPhoneManagerData;
        public XPathDataProviderSource_TestXml_OnlyForUnitTesting(string testPhoneManagerData)
        {
            _testPhoneManagerData = testPhoneManagerData;
        }

        public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            CampaignPhoneManagerModel result = new CampaignPhoneManagerModel();
            xpath = DataHelpers.UpdateXpathForTesting(xpath);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(DataHelpers.GetContentXml(_testPhoneManagerData));
            var content = doc.SelectSingleNode(xpath);

            result.DefaultPhoneNumber = content.SelectSingleNode("defaultPhoneNumber")?.InnerText ?? string.Empty;
            result.DefaultCampaignQueryStringKey = content.SelectSingleNode("defaultCampaignQueryStringKey")?.InnerText ?? string.Empty;
            result.DefaultPersistDurationInDays = Convert.ToInt32(content.SelectSingleNode("defaultPersistDurationInDays")?.InnerText ?? "0");

            return result;
        }

        public List<CampaignDetail> GetDataByXPath(string xpath)
        {
            xpath = DataHelpers.UpdateXpathForTesting(xpath);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(DataHelpers.GetContentXml(_testPhoneManagerData));
            var content = doc.SelectNodes(xpath);

            if (content != null)
                return ConvertXmlToModel(content);

            return new List<CampaignDetail>();
        }

        /// <summary>
        /// Loop XmlNodeList to convert each item to PhoneNumber
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private List<CampaignDetail> ConvertXmlToModel(XmlNodeList content)
        {
            var result = new List<CampaignDetail>();
            if (content != null)
            {
                foreach (XmlNode item in content)
                {
                    result.Add(ConvertXmlToModel(item));
                }
            }
            return result;
        }

        /// <summary>
        /// convert XmlNode to PhoneNumber
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private CampaignDetail ConvertXmlToModel(XmlNode content)
        {
            CampaignDetail result = new CampaignDetail()
            {
                Id = content.SelectSingleNode("id")?.InnerText ?? string.Empty,
                EntryPage = content.SelectSingleNode("entryPage")?.InnerText ?? string.Empty,
                AltMarketingCode = content.SelectSingleNode("altMarketingCode")?.InnerText ?? string.Empty,
                CampaignCode = content.SelectSingleNode("campaignCode")?.InnerText ?? string.Empty,
                OverwritePersistingItem = Convert.ToBoolean(content.SelectSingleNode("overwritePersistingItem")?.InnerText ?? "false"),
                DoNotPersistAcrossVisits = Convert.ToBoolean(content.SelectSingleNode("doNotPersistAcrossVisits")?.InnerText ?? "false"),
                PersistDurationOverride = Convert.ToInt32(content.SelectSingleNode("persistDurationOverride")?.InnerText ?? "0"),
                PhoneNumber = content.SelectSingleNode("phoneNumber")?.InnerText ?? string.Empty,
                Referrer = content.SelectSingleNode("referrer")?.InnerText ?? string.Empty,
                UseAltCampaignQueryStringKey = content.SelectSingleNode("useAltCampaignQueryStringKey")?.InnerText ?? string.Empty
            };

            if (string.IsNullOrEmpty(result.PhoneNumber)) // not a valid record if there is no telephone number
                return null;

            return result;
        }
    }
}
