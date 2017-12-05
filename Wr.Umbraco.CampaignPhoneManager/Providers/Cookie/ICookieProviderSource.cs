using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Inferface for Provider Source to allow for easy unit testing
    /// </summary>
    public interface ICookieProviderSource
    {
        /// <summary>
        /// Get cookie data
        /// </summary>
        /// <returns>CookieHolder</returns>
        HttpCookie GetCookie(string cookieKey);

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="model">CookieHolder</param>
        void SetCookie(HttpCookie cookie);
    }
}