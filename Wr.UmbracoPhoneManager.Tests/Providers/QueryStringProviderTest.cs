using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Specialized;
using System.Linq;
using Wr.UmbracoPhoneManager.Providers;

namespace Wr.UmbracoPhoneManager.Tests.Providers
{
    [TestClass]
    public class QueryStringProviderTest
    {
        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithNullData_ReturnsNotNull()
        {
            // Arrange
            var mockQueryStringProviderSource = MockProviders.QueryStringImplementation(null);

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetQueryStrings();

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithEmptyData_ReturnsNotNull()
        {
            // Arrange
            var mockQueryStringProviderSource = MockProviders.QueryStringImplementation(new NameValueCollection() { });

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetQueryStrings();

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithValidData_ReturnsTrue()
        {
            // Arrange
            var mockQueryStringProviderSource = MockProviders.QueryStringImplementation(new NameValueCollection
            {
                 { MockConstants.DefaultData.DefaultCampaignQuerystringKey, MockConstants.MockTestPhoneNumberData.CampaignCode}
            });

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetQueryStrings();

            // Assert
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.GetKey(0) == MockConstants.DefaultData.DefaultCampaignQuerystringKey);
            Assert.IsTrue(results.GetValues(MockConstants.DefaultData.DefaultCampaignQuerystringKey).Contains(MockConstants.MockTestPhoneNumberData.CampaignCode));
        }

        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithDangerousData_ReturnsTrue()
        {
            // Arrange
            var mockQueryStringProviderSource = MockProviders.QueryStringImplementation(new NameValueCollection
            {
                 { "dummykey", " 'or 1 = 1'"}
            });

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetQueryStrings();

            // Assert
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.GetKey(0) == "dummykey");
            Assert.IsTrue(results.GetValues("dummykey").First() == "or11");
        }
    }
}
