using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers.Storage
{
    public static class TestRepository
    {
        public static IRepository GetRepository(string testdata)
        {
            return new XPathRepository(new IXPathRepositoryImplementation_UmbracoXPathNavigatorMock(testdata));
        }
    }
}
