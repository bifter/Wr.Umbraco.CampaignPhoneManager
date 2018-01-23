using System.Collections.Specialized;
using System.Web;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class HttpContextQueryStringImplementation : IQueryStringProvider
    {
        public NameValueCollection GetQueryStrings()
        {
            return HttpContext.Current.Request.QueryString;
        }
    }
}