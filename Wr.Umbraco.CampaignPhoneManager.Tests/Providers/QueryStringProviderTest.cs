using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Specialized;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers
{
    [TestClass]
    public class QueryStringProviderTest
    {
        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithNullData_ReturnsNotNull()
        {
            // Arrange
            var mockQueryStringProviderSource = MockQueryStringProviderSource(null);

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetCleansedQueryStrings();

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithEmptyData_ReturnsNotNull()
        {
            // Arrange
            var mockQueryStringProviderSource = MockQueryStringProviderSource(new NameValueCollection() { });

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetCleansedQueryStrings();

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithValidData_ReturnsTrue()
        {
            // Arrange
            var mockQueryStringProviderSource = MockQueryStringProviderSource(new NameValueCollection
            {
                 { MockConstants.DefaultData.DefaultCampaignQuerystringKey, MockConstants.MockTestPhoneNumberData.CampaignCode}
            });

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetCleansedQueryStrings();

            // Assert
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.GetKey(0) == MockConstants.DefaultData.DefaultCampaignQuerystringKey);
            Assert.IsTrue(results.GetValues(MockConstants.DefaultData.DefaultCampaignQuerystringKey).Contains(MockConstants.MockTestPhoneNumberData.CampaignCode));
        }

        [TestMethod]
        public void QueryStringProvider_GetCleansedQueryStrings_WithDangerousData_ReturnsTrue()
        {
            // Arrange
            var mockQueryStringProviderSource = MockQueryStringProviderSource(new NameValueCollection
            {
                 { "dummykey", " 'or 1 = 1'"}
            });

            var criteria = new QueryStringProvider(mockQueryStringProviderSource.Object);

            // Act
            var results = criteria.GetCleansedQueryStrings();

            // Assert
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results.GetKey(0) == "dummykey");
            Assert.IsTrue(results.GetValues("dummykey").First() == "or11");
        }


        #region Mocks

        public static Mock<IQueryStringProviderSource> MockQueryStringProviderSource(NameValueCollection querystrings)
        {
            var mock = new Mock<IQueryStringProviderSource>();

            mock.Setup(x => x.GetQueryStrings()).Returns(querystrings);

            return mock;
        }

        #endregion
    }
}
