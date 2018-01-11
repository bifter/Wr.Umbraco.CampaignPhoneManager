using System.Collections.Specialized;

namespace Wr.UmbracoCampaignPhoneManager.Providers
{
    public interface IQueryStringProvider
    {
        NameValueCollection GetQueryStrings();
    }
}