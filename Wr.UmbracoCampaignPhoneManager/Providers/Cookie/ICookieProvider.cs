using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers
{
    public interface ICookieProvider
    {
        CookieHolder GetCookie();

        void SetCookie(CookieHolder model);
    }
}