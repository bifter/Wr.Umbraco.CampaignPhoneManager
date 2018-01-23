using System.Collections.Specialized;

namespace Wr.UmbracoPhoneManager.Providers
{
    public interface IQueryStringProvider
    {
        NameValueCollection GetQueryStrings();
    }
}