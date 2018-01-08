using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public interface ICookieProvider
    {
        CookieHolder GetCookie();

        void SetCookie(CookieHolder model);
    }
}