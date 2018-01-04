using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class HttpContextCookieImplementation : ICookieImplementation
    {
        HttpContext _httpContext;

        public HttpContextCookieImplementation()
        {
            _httpContext = HttpContext.Current;
        }

        /// <summary>
        /// Get cookie
        /// </summary>
        /// <returns>Populated HttpCookie</returns>
        public HttpCookie GetCookie(string cookieKey)
        {
            return _httpContext.Request.Cookies[cookieKey];
        }

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="model">HttpCookie</param>
        public void SetCookie(HttpCookie cookie)
        {
            _httpContext.Response.Cookies.Add(cookie);

        }
    }
}