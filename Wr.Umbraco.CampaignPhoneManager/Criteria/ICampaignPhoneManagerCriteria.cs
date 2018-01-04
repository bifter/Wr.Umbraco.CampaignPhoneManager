using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public interface ICampaignPhoneManagerCriteria
    {
        List<CampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder criteriaParameters, IRepository repository);
    }
}