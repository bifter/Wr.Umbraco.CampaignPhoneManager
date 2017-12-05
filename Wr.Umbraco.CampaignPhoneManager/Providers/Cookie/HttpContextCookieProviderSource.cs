using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class HttpContextCookieProviderSource : ICookieProviderSource
    {
        HttpContext _httpContext;

        public HttpContextCookieProviderSource()
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