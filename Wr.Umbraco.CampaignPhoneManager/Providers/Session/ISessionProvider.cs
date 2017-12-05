using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public interface ISessionProvider
    {
        OutputModel GetSession(string key = "");
        bool SetSession(OutputModel model, string key = "");
    }
}