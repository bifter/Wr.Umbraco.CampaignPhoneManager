using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public class IXPathRepositoryImplementation_UmbracoXPathNavigator : IXPathRepositoryImplementation
    {
        /*public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            CampaignPhoneManagerModel result = XmlHelper.XPathNavigatorToModel<CampaignPhoneManagerModel>(navigatorResult);

            return (result != null) ? result : new CampaignPhoneManagerModel();
        }*/

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
                            .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            T result = XmlHelper.XPathNavigatorToModel<T>(navigatorResult);

            return (result != null) ? result : new T();
        }

        public List<CampaignDetail> GetDataByXPath(string xpath)
        { 
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(xpath).Cast<XPathNavigator>();

            List<CampaignDetail> result = XmlHelper.XPathNavigatorToModel(navigatorResult);

            return (result != null) ? result.ToList() : new List<CampaignDetail>();
        }
    }
}