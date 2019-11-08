using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public class EntryPageCriteria : IPhoneManagerCriteria
    {
        /// <summary>
        /// For the selection of records that match the first page the user hits
        /// </summary>
        public EntryPageCriteria() { }

        public List<PhoneManagerCampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder _criteriaParameters, IPhoneManagerService _iphoneManagerService)
        {
            if (!string.IsNullOrEmpty(_criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage))
            {
               return _iphoneManagerService.GetMatchingRecords_Criteria_EntryPage(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.EntryPage, _criteriaParameters.RequestInfo_NotIncludingQueryStrings.EntryPage);
            }

            return new List<PhoneManagerCampaignDetail>();
        }
    }
} 