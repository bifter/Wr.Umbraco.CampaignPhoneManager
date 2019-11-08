using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public interface IPhoneManagerCriteria
    {
        List<PhoneManagerCampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder criteriaParameters, IPhoneManagerService phoneManagerService);
    }
}