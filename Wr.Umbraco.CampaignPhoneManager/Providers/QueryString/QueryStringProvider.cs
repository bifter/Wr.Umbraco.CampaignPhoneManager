using System.Collections.Specialized;
using System.Text.RegularExpressions;
using static Wr.Umbraco.CampaignPhoneManager.Helpers.ENums;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class QueryStringProvider : IQueryStringProvider
    {
        IQueryStringImplementation _queryStringImplementation;

        public QueryStringProvider()
        {
            _queryStringImplementation = new HttpContextQueryStringImplementation();
        }

        public QueryStringProvider(IQueryStringImplementation queryStringImplementation)
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