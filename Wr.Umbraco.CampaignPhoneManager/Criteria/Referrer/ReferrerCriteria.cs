using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class ReferrerCriteria : ICampaignPhoneManagerCriteria
    {
        private IRepository _repository;
        private readonly CriteriaParameterHolder _criteriaParameters;

        public ReferrerCriteria(CriteriaParameterHolder criteriaParameters)
        {
            _criteriaParameters = criteriaParameters;
            _repository = new XPathRepository();
        }

        public ReferrerCriteria(CriteriaParameterHolder criteriaParameters, IRepository repository)
        {
            _criteriaParameters = criteriaParameters;
            _repository = repository;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        { 
            if (!string.IsNullOrEmpty(_criteriaParameters.RequestInfo_NotIncludingQueryStrings.Referrer))
            {
                return _repository.GetMatchingCriteriaRecords_Referrer(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.Referrer, _criteriaParameters.RequestInfo_NotIncludingQueryStrings.Referrer);
            }

            return new List<CampaignDetail>();
        }
    }
}