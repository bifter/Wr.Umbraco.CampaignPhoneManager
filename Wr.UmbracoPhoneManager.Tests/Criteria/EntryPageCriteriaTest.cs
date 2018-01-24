using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Wr.UmbracoPhoneManager.Criteria;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;
using Wr.UmbracoPhoneManager.Tests.Providers.Storage;
using static Wr.UmbracoPhoneManager.XmlHelper;

namespace Wr.UmbracoPhoneManager.Tests.Criteria
{
    [TestClass]
    public class EntryPageCriteriaTest
    {
        [TestMethod]
        public void EntryPageCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNoEntryPage_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new PhoneManagerCampaignDetail() {}
            };

            var criteria = new EntryPageCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void EntryPageCriteria_GetMatchingRecordsFromPhoneManagerTest_WithMatchingEntryPage_ReturnsMatchingResult()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", EntryPage="umb://Homepage" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new PhoneManagerCampaignDetail() { EntryPage = dataModel.PhoneManagerCampaignDetail.First().EntryPage }
            };

            var criteria = new EntryPageCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().EntryPage, dataModel.PhoneManagerCampaignDetail.First().EntryPage);
        }
    }
}
