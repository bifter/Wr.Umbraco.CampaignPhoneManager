using System.Collections.Generic;
using Wr.UmbracoCampaignPhoneManager.Models;
using Wr.UmbracoCampaignPhoneManager.Providers.Storage;

namespace Wr.UmbracoCampaignPhoneManager.Criteria
{
    public class EntryPageCriteria : ICampaignPhoneManagerCriteria
    {
        //private IRepository _repository;
        private readonly CriteriaParameterHolder _criteriaParameters;

        public EntryPageCriteria() { }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder _criteriaParameters, IRepository _repository)
        {
            if (!string.IsNullOrEmpty(_criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage))
            {
               return _repository.GetMatchingRecords_Criteria_EntryPage(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage, _criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage);
            }

            return new List<CampaignDetail>();
        }
    }
} 