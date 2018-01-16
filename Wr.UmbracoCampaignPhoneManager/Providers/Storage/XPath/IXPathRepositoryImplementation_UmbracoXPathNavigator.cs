using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Umbraco.Core;
using Umbraco.Web;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers.Storage
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
            string homeTemplate = "//*[@id='{0}']";
            string homeXpath = "";
            var context = UmbracoContext.Current;
            if (context.IsFrontEndUmbracoRequest)
            {
                var homeNodeId = UmbracoContext.Current.PublishedContentRequest.PublishedContent.Site().Id; // using homeNodeId is a workaround for $ancestorOrSelf not working
                homeXpath = string.Format(homeTemplate, homeNodeId);
            }
            else
            {
                homeXpath = ""; // get all campaign detail records
            }

            var result = xpath.Replace("HOME_NODE_PLACEHOLDER", homeXpath);
            return result;
        }
    }
}