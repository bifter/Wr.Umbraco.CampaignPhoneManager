using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wr.UmbracoPhoneManager.Criteria.QueryString;
using System.Collections.Specialized;
using System.Linq;
using Moq;

namespace Wr.UmbracoPhoneManager.Tests.Criteria
{
    [TestClass]
    public class QuerystringCriteriaTest
    {
        [TestMethod]
        public void QuerystringCriteria_MatchingRecord_WithValidKey_ReturnsTrue()
        {
            // Arrange
            var mockQuerystring = MockQuerystringProvider(new NameValueCollection
            {
                 { MockConstants.DefaultData.DefaultCampaignQuerystringKey, MockConstants.MockTestPhoneNumberData.CampaignCode}
            });
            var mockData = DefaultMockData.MockDataProvider();

            var criteria = new QuerystringCriteria(mockData.Object, mockQuerystring.Object);

            // Act
            var results = criteria.MatchingRecords();

            // Assert
            Assert.IsTrue(results.Where(x => x.phoneNumber == MockConstants.MockTestPhoneNumberData.PhoneNumber).Count() > 0);
        }

        [TestMethod]
        public void QuerystringCriteria_MatchingRecord_WithNoMatchingKey_ReturnsFalse()
        {
            // Arrange
            var mockQuerystring = MockQuerystringProvider(new NameValueCollection
            {
                 { "grrrr", "test"}
            });
            var mockData = DefaultMockData.MockDataProvider();
            var criteria = new QuerystringCriteria(mockData.Object, mockQuerystring.Object);

            // Act
            var results = criteria.MatchingRecords();

            // Assert
            Assert.IsFalse(results.Count() > 0);
        }

        [TestMethod]
        public void QuerystringCriteria_MatchingRecord_WithNoQuerystring_ReturnsFalse()
        {
            // Arrange
            var mockQuerystring = MockQuerystringProvider(new NameValueCollection { });
            var mockData = DefaultMockData.MockDataProvider();
            var criteria = new QuerystringCriteria(mockData.Object, mockQuerystring.Object);

            // Act
            var results = criteria.MatchingRecords();

            // Assert
            Assert.IsFalse(results.Count() > 0);
        }


        #region Mocks

        private static Mock<IQuerystringProvider> MockQuerystringProvider(NameValueCollection querystrings)
        {
            var mock = new Mock<IQuerystringProvider>();

            mock.Setup(x => x.GetQuerystring()).Returns(querystrings);

            return mock;
        }

        #endregion
    }

}
