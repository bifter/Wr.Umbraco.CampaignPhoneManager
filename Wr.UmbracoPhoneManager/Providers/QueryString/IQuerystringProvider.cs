using System.Collections.Specialized;

namespace Wr.UmbracoPhoneManager.Providers
{
    public interface IQueryStringProvider
    {
        /// <summary>
        /// A collection of querystring keys and values
        /// </summary>
        /// <returns></returns>
        NameValueCollection GetCleansedQueryStrings();

    }
}