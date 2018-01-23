using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers.Storage
{
    public interface IXPathRepositoryImplementation
    {

        PhoneManagerModel LoadDefaultSettings(string xpath);

        T GetDataByXPath<T>(string xpath) where T : class, new();

        List<PhoneManagerCampaignDetail> GetDataByXPath(string xpath);
    }
}