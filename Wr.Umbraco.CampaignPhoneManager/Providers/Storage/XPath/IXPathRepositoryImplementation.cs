using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public interface IXPathRepositoryImplementation
    {

        CampaignPhoneManagerModel LoadDefaultSettings(string xpath);


        List<CampaignDetail> GetDataByXPath(string xpath);
    }
}