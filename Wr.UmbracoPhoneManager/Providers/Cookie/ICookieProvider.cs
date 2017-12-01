using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers.Cookie
{
    public interface ICookieProvider
    {
        /// <summary>
        /// Get cookie data and map to CookieHolder model
        /// </summary>
        /// <returns>CookieHolder</returns>
        CookieHolder GetCookie();

        /// <summary>
        /// Set cookie adding subvalues for the PhoneManagerResultModel values
        /// </summary>
        /// <param name="model">CookieHolder</param>
        void SetCookie(CookieHolder model);
    }
}