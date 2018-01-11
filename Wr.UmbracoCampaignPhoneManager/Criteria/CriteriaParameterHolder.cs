using System.Collections.Specialized;
using Wr.UmbracoCampaignPhoneManager.Models;
using static Wr.UmbracoCampaignPhoneManager.Helpers.ENums;

namespace Wr.UmbracoCampaignPhoneManager.Criteria
{
    /// <summary>
    /// Returns 'injection safe' QueryStrings values
    /// </summary>
    public partial class CriteriaParameterHolder
    {
        public CampaignDetail RequestInfo_NotIncludingQueryStrings { get; set; }

        /// <summary>
        /// Included here mainly to allow for easier unit testing parts of the app
        /// </summary>
        private NameValueCollection _querystrings;

        /// <summary>
        /// Returns 'injection safe' values
        /// </summary>
        public NameValueCollection CleansedQueryStrings
        {
            get
            {
                return _querystrings;
            }
            set
            {
                // only set 'injection safe' strings
                _querystrings = value.ToSafeCollection(ProviderType.QueryString);
            }
        }
    }
}