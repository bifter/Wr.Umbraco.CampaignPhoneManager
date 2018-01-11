using System.Collections.Generic;
using Wr.UmbracoCampaignPhoneManager.Models;
using Wr.UmbracoCampaignPhoneManager.Providers.Storage;

namespace Wr.UmbracoCampaignPhoneManager.Criteria
{
    public interface ICampaignPhoneManagerCriteria
    {
        List<CampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder criteriaParameters, IRepository repository);
    }
}