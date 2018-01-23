using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{
    public interface ISessionProvider
    {
        OutputModel GetSession(string key = "");
        bool SetSession(OutputModel model, string key = "");
    }
}