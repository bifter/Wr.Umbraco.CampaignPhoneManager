using System;
using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Logic for all XPathDataProvider Sources
    /// </summary>
    public partial class XPathDataProvider : XPathDataProviderBase, IDataProvider
    {
        private IXPathCriteriaDataProvider _dataProvider;
        private IXPathDataProviderSource _dataSource;

        public XPathDataProvider()
        {
            _dataSource = new XPathDataProviderSource_UmbracoGetXPathNavigator();
        }

        public XPathDataProvider(IXPathCriteriaDataProvider Provider)
        {
            _dataProvider = dataProvider;
        }

        public Type InterfaceSelector()
        {
            return typeof(IXPathCriteriaDataProvider);
        }

        public CampaignPhoneManagerModel GetDefaultSettings()
        {
            return _dataSource.GetDefaultSettings();
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            throw new NotImplementedException();
        }

        
    }
}