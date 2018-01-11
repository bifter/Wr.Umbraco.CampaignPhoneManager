using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers.Storage
{
    public class IXPathRepositoryImplementation_UmbracoXPathNavigator : IXPathRepositoryImplementation
    {
        public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            
            /*var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            CampaignPhoneManagerModel result = XmlHelper.XPathNavigatorToModel<CampaignPhoneManagerModel>(navigatorResult);

            System.Diagnostics.Debug.WriteLine("LoadDefaultSettings DefaultPhoneNumber: " + result.DefaultPhoneNumber);*/

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
                            .Select(UpdateXPathPrefix(xpath)).Cast<XPathNavigator>().FirstOrDefault();

            T result = null;
            if (navigatorResult != null)
            {
                result = XmlHelper.XPathNavigatorToModel<T>(navigatorResult);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("navigatorResult is Null!");
            }

            return (result != null) ? result : new T();
        }

        public List<CampaignDetail> GetDataByXPath(string xpath)
        {
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(UpdateXPathPrefix(xpath)).Cast<XPathNavigator>();

            List<CampaignDetail> result = XmlHelper.XPathNavigatorToModel(navigatorResult);

            return (result != null) ? result.ToList() : new List<CampaignDetail>();
        }

        private string UpdateXPathPrefix(string xpath)
        {
            var currentNodeId = UmbracoContext.Current.PageId;
            var result = xpath.Replace("$ancestorOrSelf", "//node[@id=" + currentNodeId + "]");
            return result;
        }
    }


}