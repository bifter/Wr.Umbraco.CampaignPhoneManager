using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;
using static Wr.UmbracoPhoneManager.Helpers.ENums;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public class ReferrerCriteria : IPhoneManagerCriteria
    {
        /// <summary>
        /// For the selection of records that match the referrer passed with the request
        /// </summary>
        public ReferrerCriteria () { }

        public List<PhoneManagerCampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder _criteriaParameters, IRepository _repository)
        {
            var safeReferrer = _criteriaParameters.RequestInfo_NotIncludingQueryStrings.Referrer.ToSafeString(ProviderType.Referrer);
            if (!string.IsNullOrEmpty(safeReferrer))
            {
                return _repository.GetMatchingCriteriaRecords_Referrer(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.Referrer, safeReferrer);
            }

            return new List<PhoneManagerCampaignDetail>();
        }
    }
}