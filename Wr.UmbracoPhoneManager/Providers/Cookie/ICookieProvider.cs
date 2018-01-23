using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{
    public interface ICookieProvider
    {
        CookieHolder GetCookie();

        void SetCookie(CookieHolder model);
    }
}