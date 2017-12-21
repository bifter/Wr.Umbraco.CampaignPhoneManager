using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class EntryPageCriteria : ICampaignPhoneManagerCriteria
    {
        private IDataProvider _iDataProvider;
        private IUmbracoProvider _umbracoProvider;
        private readonly string _entryPage;

        public EntryPageCriteria()
        {
            _umbracoProvider = new UmbracoProvider();
            _entryPage = _umbracoProvider.GetCurrentPageId();

            _iDataProvider = new EntryPageCriteria_DataSource_XPath(_entryPage);
        }

        public EntryPageCriteria(CriteriaDIHolder criteriaDIHolder)
        {
            if (criteriaDIHolder != null)
            {
                _umbracoProvider = criteriaDIHolder.UmbracoProvider;
                _entryPage = _umbracoProvider.GetCurrentPageId();
                _iDataProvider = criteriaDIHolder.DataProvider;
            }
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            if (!string.IsNullOrEmpty(_entryPage))
            {
               return _iDataProvider.GetMatchingRecordsFromPhoneManager();
            }

            return new List<CampaignDetail>();
        }
    }
} 