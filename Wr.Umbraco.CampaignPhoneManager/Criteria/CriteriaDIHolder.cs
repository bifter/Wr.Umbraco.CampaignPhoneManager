using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public partial class CriteriaDIHolder
    {
        public IDataProvider DataProvider { get; set; }

        public IUmbracoProvider UmbracoProvider { get; set; }

        public QueryStringProvider QueryStringProvider { get; set; }
    }
}