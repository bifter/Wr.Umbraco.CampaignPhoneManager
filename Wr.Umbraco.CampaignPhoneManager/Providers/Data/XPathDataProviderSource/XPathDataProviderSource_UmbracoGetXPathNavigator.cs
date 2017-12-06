﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class XPathDataProviderSource_UmbracoGetXPathNavigator : IXPathDataProviderSource
    {

        private UmbracoHelper _umbracoHelper;

        public XPathDataProviderSource_UmbracoGetXPathNavigator()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            CampaignPhoneManagerModel result = XmlHelper.XPathNavigatorToModel<CampaignPhoneManagerModel>(navigatorResult);

            return (result != null) ? result : new CampaignPhoneManagerModel();
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