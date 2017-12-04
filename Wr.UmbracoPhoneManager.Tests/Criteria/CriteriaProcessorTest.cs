using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Wr.UmbracoPhoneManager.Criteria;

namespace Wr.UmbracoPhoneManager.Tests.Criteria
{
    [TestClass]
    public class CriteriaProcessorTest
    {
        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecord_()
        {
            // Arrange
            var mockQuerystring = MockQuerystringProvider(new NameValueCollection
            {
                 { MockConstants.DefaultData.DefaultCampaignQuerystringKey, MockConstants.MockTestPhoneNumberData.CampaignCode}
            });
            var mockData = DefaultMockData.MockDataProvider();

            var criteria = new CriteriaProcessor(mockData.Object, mockQuerystring.Object);

            // Act
            var results = criteria.GetMatchingRecord();

            // Assert
            Assert.IsTrue(results.Where(x => x.phoneNumber == MockConstants.MockTestPhoneNumberData.PhoneNumber).Count() > 0);
        }
    }
}
