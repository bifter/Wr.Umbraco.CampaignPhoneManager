using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Wr.UmbracoPhoneManager.Criteria;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Tests.Criteria
{
    [TestFixture]
    public class QueryStringCriteriaTest
    {
        [Test]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNullQS_ReturnsEmptyResults()
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
                RequestInfo_NotIncludingQueryStrings = new PhoneManagerCampaignDetail() { }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [Test]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNonMatchingQS_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() {{"dummykey", "nothing"}, { "dummykey2", "nothingagain" }},
                RequestInfo_NotIncludingQueryStrings = new PhoneManagerCampaignDetail() { }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);      
        }

        [Test]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithMatchingQS_ReturnsCorrectResults()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "dummykey", "nothing" }, { "fsource", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new PhoneManagerCampaignDetail() { EntryPage = dataModel.PhoneManagerCampaignDetail.First().EntryPage }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().CampaignCode, dataModel.PhoneManagerCampaignDetail.First().CampaignCode);
        }

        [Test]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithMatchingAltQS_ReturnsCorrectResults()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", UseAltCampaignQueryStringKey = "altkey" } };
            var testPhoneManagerData = dataModel.ToXmlString();// dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "dummykey", "nothing" }, { "altkey", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new PhoneManagerCampaignDetail() { EntryPage = dataModel.PhoneManagerCampaignDetail.First().EntryPage }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().CampaignCode, dataModel.PhoneManagerCampaignDetail.First().CampaignCode);
        }
    }
}
