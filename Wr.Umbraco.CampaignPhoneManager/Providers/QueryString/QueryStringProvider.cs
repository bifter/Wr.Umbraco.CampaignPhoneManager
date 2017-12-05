using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class QueryStringProvider
    {
        IQueryStringProviderSource _queryStringProviderSource;

        public QueryStringProvider()
        {
            _queryStringProviderSource = new HttpContextQueryStringProviderSource();
        }

        public QueryStringProvider(IQueryStringProviderSource queryStringProviderSource)
        {
            _queryStringProviderSource = queryStringProviderSource;
        }

        public NameValueCollection GetCleansedQueryStrings()
        {
            var qs = _queryStringProviderSource.GetQueryStrings();
            if (qs != null)
            {
                if (qs.Count > 0)
                {
                    NameValueCollection newQs = new NameValueCollection();
                    foreach (string key in qs)
                    {
                        var cleanKey = CleanString(key);
                        var cleanValue = CleanString(qs[cleanKey]);
                        if (!string.IsNullOrEmpty(cleanKey) && !string.IsNullOrEmpty(cleanValue)) // only use qs where they have a value
                        {
                            newQs.Add(cleanKey, cleanValue);
                        }
                        return newQs;
                    }
                }
            }
            return new NameValueCollection();
        }

        private string CleanString(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return Regex.Replace(value, "[^A-Za-z0-9_-]", ""); // allow alpha-numeric and _ - 

            return string.Empty;
        }
    }
}