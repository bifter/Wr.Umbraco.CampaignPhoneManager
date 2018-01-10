using System.Collections.Specialized;
using System.Text.RegularExpressions;
using static Wr.Umbraco.CampaignPhoneManager.Helpers.ENums;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
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