using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wr.UmbracoPhoneManager.Criteria.Referrer
{
    public class HttpContextReferrerProvider : IReferrerProvider
    {
        public string GetReferrer()
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                var hostname = HttpContext.Current.Request.Url.Host.ToString().ToLower(); // self
                string referrer = HttpContext.Current.Request.UrlReferrer.Host.ToLower() ?? string.Empty;
                if (!string.IsNullOrEmpty(referrer) && !hostname.Equals(referrer)) // Ignore local referring
                {
                    return referrer;
                }
            }

            return string.Empty;
        }
    }
}