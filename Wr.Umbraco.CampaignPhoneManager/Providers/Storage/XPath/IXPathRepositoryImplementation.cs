using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public interface IXPathRepositoryImplementation
    {

        CampaignPhoneManagerModel LoadDefaultSettings(string xpath);

        T GetDataByXPath<T>(string xpath) where T : class, new();

        List<CampaignDetail> GetDataByXPath(string xpath);
    }
}