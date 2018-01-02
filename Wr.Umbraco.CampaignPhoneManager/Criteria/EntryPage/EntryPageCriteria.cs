using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class EntryPageCriteria : ICampaignPhoneManagerCriteria
    {
        private IRepository _repository;
        private readonly CriteriaParameterHolder _criteriaParameters;

        public EntryPageCriteria(CriteriaParameterHolder criteriaParameters)
        {
            _criteriaParameters = criteriaParameters;
            _repository = new XPathRepository();
        }

        public EntryPageCriteria(CriteriaParameterHolder criteriaParameters, IRepository repository)
        {
            _criteriaParameters = criteriaParameters;
            _repository = repository;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            if (!string.IsNullOrEmpty(_criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage))
            {
               return _repository.GetMatchingRecords_Criteria_EntryPage(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage, _criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage);
            }

            return new List<CampaignDetail>();
        }
    }
} 