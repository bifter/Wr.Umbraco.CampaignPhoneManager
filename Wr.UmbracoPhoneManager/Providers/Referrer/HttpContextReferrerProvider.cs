using System.Web;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class HttpContextReferrerProvider : IReferrerProvider
    {
        public string GetReferrer()
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                string referrer = HttpContext.Current.Request.UrlReferrer.Host.ToLower() ?? string.Empty;
                return referrer;
            }

            return string.Empty;
        }

        public string GetReferrerOrNone()
        {
            string referrer = GetReferrer();
            return (!string.IsNullOrEmpty(referrer)) ? referrer : "none";
        }
    }
}