using System.Collections.Specialized;
using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class HttpContextQueryStringImplementation : IQueryStringProvider
    {
        public NameValueCollection GetQueryStrings()
        {
            return HttpContext.Current.Request.QueryString;
        }
    }
}