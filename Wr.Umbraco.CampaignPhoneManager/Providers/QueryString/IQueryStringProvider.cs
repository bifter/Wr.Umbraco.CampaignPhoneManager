using System.Collections.Specialized;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public interface IQueryStringProvider
    {
        NameValueCollection GetQueryStrings();
    }
}