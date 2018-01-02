using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Data source using Umbracos GetXPathNavigator to get cached campaign phone manager content
    /// </summary>
    public class XPathDataSource_UmbracoGetXPathNavigator : XPathDataProviderBase, IXPathDataSource
    {
        private UmbracoHelper _umbracoHelper;

        public XPathDataSource_UmbracoGetXPathNavigator()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        private CampaignPhoneManagerModel _defaultSettings { get; set; }

        public CampaignPhoneManagerModel GetDefaultSettings()
        {
            if (_defaultSettings == null)
            {
                _defaultSettings = LoadDefaultSettings(xpath4DefaultCampaignPhoneManagerSettings);
            }
            return _defaultSettings;
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