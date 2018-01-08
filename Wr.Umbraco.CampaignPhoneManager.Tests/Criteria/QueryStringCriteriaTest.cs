using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Criteria;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers.Storage;
using static Wr.Umbraco.CampaignPhoneManager.XmlHelper;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Criteria
{
    [TestClass]
    public class QueryStringCriteriaTest
    {
        [TestMethod]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNullQS_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = TestRepository.GetRepository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNonMatchingQS_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = TestRepository.GetRepository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() {{"dummykey", "nothing"}, { "dummykey2", "nothingagain" }},
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);      
        }

        [TestMethod]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithMatchingQS_ReturnsCorrectResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = TestRepository.GetRepository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "dummykey", "nothing" }, { "fsource", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { EntryPage = dataModel.CampaignDetail.First().EntryPage }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().CampaignCode, dataModel.CampaignDetail.First().CampaignCode);
        }

        [TestMethod]
        public void QueryStringCriteria_GetMatchingRecordsFromPhoneManagerTest_WithMatchingAltQS_ReturnsCorrectResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", UseAltCampaignQueryStringKey = "altkey" } };
            var testPhoneManagerData = dataModel.ToXmlString();// dataModel.ToXmlString();

            IRepository _repository = TestRepository.GetRepository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "dummykey", "nothing" }, { "altkey", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { EntryPage = dataModel.CampaignDetail.First().EntryPage }
            };

            var criteria = new QueryStringCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().CampaignCode, dataModel.CampaignDetail.First().CampaignCode);
        }
    }
}
