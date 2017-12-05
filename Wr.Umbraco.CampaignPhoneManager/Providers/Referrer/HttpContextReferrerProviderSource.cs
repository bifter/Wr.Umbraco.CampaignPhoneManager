using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class HttpContextReferrerProviderSource : IReferrerProviderSource
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
    }
}