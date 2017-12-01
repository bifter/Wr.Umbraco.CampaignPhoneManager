using System.Collections.Specialized;
using System.Web;

namespace Wr.UmbracoPhoneManager.Criteria.QueryString
{

    public class HttpContextQuerystringProvider : IQuerystringProvider
    {
        public NameValueCollection GetQuerystring()
        {
            return HttpContext.Current.Request.QueryString;
        }
    }

}