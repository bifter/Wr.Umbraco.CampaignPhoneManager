using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public class EntryPageCriteria : IPhoneManagerCriteria
    {
        public EntryPageCriteria() { }

        public List<PhoneManagerCampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder _criteriaParameters, IRepository _repository)
        {
            if (!string.IsNullOrEmpty(_criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage))
            {
               return _repository.GetMatchingRecords_Criteria_EntryPage(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.EntryPage, _criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage);
            }

            return new List<PhoneManagerCampaignDetail>();
        }
    }
} 