using Moq;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Tests
{
    public static class MockProviders
    {
        /// <summary>
        /// Holder for CampaignPhoneManagerApp Parameters
        /// </summary>
        public class CampaignPhoneManagerAppParamHolder
        {
            public readonly ICookieProvider CookieProvider;
            public readonly IRepository RepositoryProvider;
            public readonly QueryStringProvider QueryStringProvider;
            public readonly ReferrerProvider ReferrerProvider;
            public readonly ISessionProvider SessionProvider;
            public readonly IUmbracoProvider UmbracoProvider;

            public CampaignPhoneManagerAppParamHolder(CookieHolder cookieHolder, string repositoryTestPhoneManagerData, NameValueCollection queryStringCollection, string referrerString, OutputModel sessionModel, string umbracoCurrentPageId)
            {
                CookieProvider = CookieProvider(cookieHolder).Object;
                RepositoryProvider = Repository(repositoryTestPhoneManagerData);
                QueryStringProvider = new QueryStringProvider(QueryStringImplementation(queryStringCollection).Object);
                ReferrerProvider = new ReferrerProvider(ReferrerImplementation(referrerString).Object);
                SessionProvider = SessionProvider(sessionModel).Object;
                UmbracoProvider = UmbracoProvider(umbracoCurrentPageId).Object;
            }

        }

        public static IRepository Repository(string testdata)
        {
            return new XPathRepository(new IXPathRepositoryImplementation_UmbracoXPathNavigatorMock(testdata));
        }

        public static Mock<ICookieProvider> CookieProvider(CookieHolder model)
        {
            var mock = new Mock<ICookieProvider>();

            mock.Setup(x => x.GetCookie()).Returns(model);

            return mock;
        }

        /*public static Mock<IQueryStringProvider> QueryStringProvider(NameValueCollection model)
        {
            var mock = new Mock<IQueryStringProvider>();

            mock.Setup(x => x.GetQueryStrings()).Returns(model);

            return mock;
        }*/

        public static Mock<IQueryStringImplementation> QueryStringImplementation(NameValueCollection querystrings)
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

        public static Mock<IReferrerImplementation> ReferrerImplementation(string model)
        {
            var mock = new Mock<IReferrerImplementation>();

            mock.Setup(x => x.GetReferrer()).Returns(model);

            return mock;
        }

        public static Mock<ISessionProvider> SessionProvider(OutputModel model)
        {
            var mock = new Mock<ISessionProvider>();

            mock.Setup(x => x.GetSession("")).Returns(model);

            return mock;
        }

        public static Mock<IUmbracoProvider> UmbracoProvider(string model)
        {
            var mock = new Mock<IUmbracoProvider>();

            mock.Setup(x => x.GetCurrentPageId()).Returns(model);

            return mock;
        }
    }
}
