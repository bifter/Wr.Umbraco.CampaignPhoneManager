using System.Collections.Specialized;
using static Wr.UmbracoPhoneManager.Helpers.ENums;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class QueryStringProvider : IQueryStringProvider
    {
        IQueryStringProvider _queryStringImplementation;

        public QueryStringProvider()
        {
            _queryStringImplementation = new HttpContextQueryStringImplementation();
        }

        public QueryStringProvider(IQueryStringProvider queryStringImplementation)
        {
            _queryStringImplementation = queryStringImplementation;
        }

        /// <summary>
        /// Returns request querystrings
        /// </summary>
        /// <returns></returns>
        public NameValueCollection GetQueryStrings()
        {
            return _queryStringImplementation.GetQueryStrings().ToSafeCollection(ProviderType.QueryString);
        }
    }
}