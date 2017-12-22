using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public partial class CriteriaParameterHolder
    {
        public CampaignDetail RequestInfoNotIncludingQueryStrings { get; set; }
        public NameValueCollection CleansedQueryStrings { get; set; }
    }
}