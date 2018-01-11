using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers
{
    public interface ISessionProvider
    {
        OutputModel GetSession(string key = "");
        bool SetSession(OutputModel model, string key = "");
    }
}