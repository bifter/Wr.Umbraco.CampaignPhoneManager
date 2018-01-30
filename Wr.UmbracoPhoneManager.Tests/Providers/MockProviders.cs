using Moq;
using System.Collections.Specialized;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers;
using Wr.UmbracoPhoneManager.Providers.Storage;
using Wr.UmbracoPhoneManager.Tests.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Tests
{
    public static class MockProviders
    {
        /// <summary>
        /// Holder for PhoneManagerApp Parameters
        /// </summary>
        public class PhoneManagerAppParamHolder
        {
            public readonly ICookieProvider CookieProvider;
            public readonly IRepository RepositoryProvider;
            public readonly QueryStringProvider QueryStringProvider;
            public readonly ReferrerProvider ReferrerProvider;
            public readonly ISessionProvider SessionProvider;
            public readonly IUmbracoProvider UmbracoProvider;

            public PhoneManagerAppParamHolder(CookieHolder cookieHolder, string repositoryTestPhoneManagerData, NameValueCollection queryStringCollection, string referrerString, OutputModel sessionModel, string umbracoCurrentPageId)
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

        public static Mock<IQueryStringProvider> QueryStringImplementation(NameValueCollection querystrings)
        {
            var mock = new Mock<IQueryStringProvider>();

            mock.Setup(x => x.GetQueryStrings()).Returns(querystrings);

            return mock;
        }

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
