using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class HttpContextQueryStringProvider : IQueryStringProvider
    {
        public NameValueCollection GetCleansedQueryStrings()
        {
            var qs = HttpContext.Current.Request.QueryString;
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