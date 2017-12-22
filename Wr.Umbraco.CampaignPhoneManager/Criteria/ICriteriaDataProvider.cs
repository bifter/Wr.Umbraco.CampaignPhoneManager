using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public interface ICriteriaDataProvider
    {
        List<CampaignDetail> GetMatchingRecordsFromPhoneManager();
    }
}