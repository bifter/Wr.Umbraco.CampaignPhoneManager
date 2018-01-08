using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers.Storage
{
    public class IXPathRepositoryImplementation_UmbracoXPathNavigatorMock : IXPathRepositoryImplementation
    {
        private readonly string _testPhoneManagerData;

        public IXPathRepositoryImplementation_UmbracoXPathNavigatorMock(string testPhoneManagerData)
        {
            _testPhoneManagerData = testPhoneManagerData.Replace("<?xml version=\"1.0\"?>", "");
        }

        public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            return GetDataByXPath<CampaignPhoneManagerModel>(xpath);
        }

        /// <summary>
        /// Generic method to return single instance of matching Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public T GetDataByXPath<T>(string xpath) where T : class, new()
        {
            xpath = DataHelpers.UpdateXpathForTesting(xpath);

            var textReader = new StringReader(DataHelpers.GetContentXml(_testPhoneManagerData));
            var navigatorResult = new XPathDocument(textReader).CreateNavigator()
                                .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            T result = XmlHelper.XPathNavigatorToModel<T>(navigatorResult);

            return (result != null) ? result : new T();
        }


        public List<CampaignDetail> GetDataByXPath(string xpath)
        {
            xpath = DataHelpers.UpdateXpathForTesting(xpath);

            var textReader = new StringReader(DataHelpers.GetContentXml(_testPhoneManagerData));
            var navigatorResult = new XPathDocument(textReader).CreateNavigator()
                                .Select(xpath).Cast<XPathNavigator>();

            List<CampaignDetail> result = XmlHelper.XPathNavigatorToModel(navigatorResult);

            return (result != null) ? result.ToList() : new List<CampaignDetail>();
        }
    }
}
