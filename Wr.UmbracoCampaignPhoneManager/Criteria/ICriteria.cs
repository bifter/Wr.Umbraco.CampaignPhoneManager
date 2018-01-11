using System.Collections.Generic;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Criteria
{
    public interface ICriteria
    {
        List<CampaignDetail> GetMatchingRecordsFromPhoneManager();
    }
}