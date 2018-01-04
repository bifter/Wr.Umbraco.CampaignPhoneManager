using System.Collections.Specialized;
using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class HttpContextQueryStringImplementation : IQueryStringImplementation
    {
        public NameValueCollection GetQueryStrings()
        {
            return HttpContext.Current.Request.QueryString;
        }
    }
}