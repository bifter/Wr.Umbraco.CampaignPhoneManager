using System;
using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Logic for all XPathDataProvider Sources
    /// </summary>
    public class XPathDataProvider: XPathDataProviderBase, IDataProvider
    {
        private IXPathCriteriaDataProvider _dataProvider;
        private IXPathDataSource _dataSource;

        public XPathDataProvider()
        {
            _dataSource = new XPathDataSource_UmbracoGetXPathNavigator();
        }

        public XPathDataProvider(IXPathCriteriaDataProvider dataProvider)
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

        public virtual List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            throw new NotImplementedException();
        }

        
    }
}