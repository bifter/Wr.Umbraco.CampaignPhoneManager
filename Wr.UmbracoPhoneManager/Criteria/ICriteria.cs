using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public interface ICriteria
    {
        List<PhoneManagerCampaignDetail> GetMatchingRecordsFromPhoneManager();
    }
}