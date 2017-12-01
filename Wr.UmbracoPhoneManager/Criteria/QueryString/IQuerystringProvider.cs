using System.Collections.Specialized;

namespace Wr.UmbracoPhoneManager.Criteria.QueryString
{
    public interface IQuerystringProvider
    {
        /// <summary>
        /// A collection of querystring keys and values
        /// </summary>
        /// <returns></returns>
        NameValueCollection GetQuerystring();
    }
}