using Moq;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests
{
    public static class MockProviders
    {
        public static Mock<CookieProvider> MockCookieProvider(CookieHolder model)
        {
            var mock = new Mock<CookieProvider>();

            mock.Setup(x => x.GetCookie()).Returns(model);

            return mock;
        }

        /*public static Mock<IQueryStringProvider> MockQueryStringProvider(NameValueCollection model)
        {
            var mock = new Mock<IQueryStringProvider>();

            mock.Setup(x => x.GetQueryStrings()).Returns(model);

            return mock;
        }*/

        public static Mock<IQueryStringImplementation> MockQueryStringImplementation(NameValueCollection querystrings)
        {
            var mock = new Mock<IQueryStringImplementation>();

            mock.Setup(x => x.GetQueryStrings()).Returns(querystrings);

            return mock;
        }

        /*public static Mock<IReferrerProvider> MockReferrerProvider(string model)
        {
            var mock = new Mock<IReferrerProvider>();

            mock.Setup(x => x.GetReferrerOrNone()).Returns(model);

            return mock;
        }*/

        public static Mock<IReferrerImplementation> MockReferrerImplementation(string model)
        {
            var mock = new Mock<IReferrerImplementation>();

            mock.Setup(x => x.GetReferrer()).Returns(model);

            return mock;
        }

        public static Mock<ISessionProvider> MockSessionProvider(OutputModel model)
        {
            var mock = new Mock<ISessionProvider>();

            mock.Setup(x => x.GetSession("")).Returns(model);

            return mock;
        }

        public static Mock<IUmbracoProvider> MockUmbracoProvider(string model)
        {
            var mock = new Mock<IUmbracoProvider>();

            mock.Setup(x => x.GetCurrentPageId()).Returns(model);

            return mock;
        }
    }
}
