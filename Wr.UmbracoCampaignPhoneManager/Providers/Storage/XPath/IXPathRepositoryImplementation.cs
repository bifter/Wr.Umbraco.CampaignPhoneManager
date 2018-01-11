using System.Collections.Generic;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers.Storage
{
    public interface IXPathRepositoryImplementation
    {

        CampaignPhoneManagerModel LoadDefaultSettings(string xpath);

        T GetDataByXPath<T>(string xpath) where T : class, new();

        List<CampaignDetail> GetDataByXPath(string xpath);
    }
}