using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public partial class CriteriaParameterHolder
    {
        public CampaignDetail RequestInfo_NotIncludingQueryStrings { get; set; }

        /// <summary>
        /// Included here mainly to allow for easier unit testing parts of the app
        /// </summary>
        public NameValueCollection CleansedQueryStrings { get; set; }
    }
}