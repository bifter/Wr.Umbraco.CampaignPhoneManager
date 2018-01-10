using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public class IXPathRepositoryImplementation_UmbracoXPathNavigator : IXPathRepositoryImplementation
    {
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
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(UmbracoXPathAncestorOrSelfWorkaround(xpath)).Cast<XPathNavigator>().FirstOrDefault();

            T result = XmlHelper.XPathNavigatorToModel<T>(navigatorResult);

            return (result != null) ? result : new T();
        }

        public List<CampaignDetail> GetDataByXPath(string xpath)
        {
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(UmbracoXPathAncestorOrSelfWorkaround(xpath)).Cast<XPathNavigator>();

            List<CampaignDetail> result = XmlHelper.XPathNavigatorToModel(navigatorResult);

            return (result != null) ? result.ToList() : new List<CampaignDetail>();
        }

        /// <summary>
        /// For some reason '$ancestorOrSelf' is not working as it should so replacing this with //node[@id='']
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public string UmbracoXPathAncestorOrSelfWorkaround(string xpath)
        {
            var currentNodeId = UmbracoContext.Current.PageId;
            var result = xpath.Replace("$ancestorOrSelf", "//node[@id='" + currentNodeId + "']");
            return result;
        }
    }
}