using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public abstract class XPathDataProviderBase
    {
        internal const string baseXpath = "$ancestorOrSelf/ancestor-or-self::home[position()=1]//";

        internal static string xpath4CampaignDetailHolder = string.Format("{0}{1}/{2}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail) + "[{0}]";

        public static string xpath4DefaultCampaignPhoneManagerSettings = string.Format("{0}{1}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager);

        /*private CampaignPhoneManagerModel _defaultSettings { get; set; }

        public CampaignPhoneManagerModel GetDefaultSettings()
        {
            if (_defaultSettings == null)
            {
                _defaultSettings = LoadDefaultSettings(xpath4DefaultCampaignPhoneManagerSettings);
            }
            return _defaultSettings;
        }

        public virtual CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(xpath).Cast<XPathNavigator>().FirstOrDefault();

            CampaignPhoneManagerModel result = XmlHelper.XPathNavigatorToModel<CampaignPhoneManagerModel>(navigatorResult);

            return (result != null) ? result : new CampaignPhoneManagerModel();
        }

        public virtual List<CampaignDetail> GetDataByXPath(string xpath)
        {
            var navigatorResult = UmbracoContext.Current.ContentCache.GetXPathNavigator()
                            .Select(xpath).Cast<XPathNavigator>();

            List<CampaignDetail> result = XmlHelper.XPathNavigatorToModel(navigatorResult);

            return (result != null) ? result.ToList() : new List<CampaignDetail>();
        }*/

    }
}