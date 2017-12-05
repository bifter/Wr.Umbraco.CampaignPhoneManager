using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class HttpContextQueryStringProviderSource : IQueryStringProviderSource
    {
        public NameValueCollection GetQueryStrings()
        {
            return HttpContext.Current.Request.QueryString;
        }
    }
}