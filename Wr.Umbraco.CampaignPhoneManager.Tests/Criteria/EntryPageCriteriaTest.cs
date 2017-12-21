using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wr.Umbraco.CampaignPhoneManager.Criteria;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Criteria
{
    [TestClass]
    public class EntryPageCriteriaTest
    {
        [TestMethod]
        public void EntryPageCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNotEntryPage_ReturnsTrue()
        {
            // Arrange
            var mock = new Mock<IUmbracoProvider>();
            mock.Setup(x => x.GetCurrentPageId()).Returns("");

            

            var criteria = new EntryPageCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
        }
    }
}
