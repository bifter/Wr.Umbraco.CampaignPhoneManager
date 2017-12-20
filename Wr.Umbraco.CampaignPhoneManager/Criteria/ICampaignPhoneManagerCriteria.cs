using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public interface ICampaignPhoneManagerCriteria
    {
        List<CampaignDetail> GetMatchingRecordsFromPhoneManager();
    }
}