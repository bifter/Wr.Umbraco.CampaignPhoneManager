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

        public EntryPageCriteria(CriteriaParameterHolder criteriaParameters)
        {
            _iDataProvider = new XPathDataProvider(new EntryPageCriteria_DataSource_XPath(criteriaParameters));
        }

        public EntryPageCriteria(CriteriaParameterHolder criteriaParameters, IDataProvider iDataProvider)
        {
            _iDataProvider = iDataProvider;
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