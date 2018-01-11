using System.Collections.Specialized;
using System.Web;

namespace Wr.UmbracoCampaignPhoneManager.Providers
{
    public class HttpContextQueryStringImplementation : IQueryStringProvider
    {
        public NameValueCollection GetQueryStrings()
        {
            return HttpContext.Current.Request.QueryString;
        }
    }
}