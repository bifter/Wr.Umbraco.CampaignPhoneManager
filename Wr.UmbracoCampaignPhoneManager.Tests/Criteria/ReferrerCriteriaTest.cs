using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Wr.UmbracoCampaignPhoneManager.Criteria;
using Wr.UmbracoCampaignPhoneManager.Models;
using Wr.UmbracoCampaignPhoneManager.Providers.Storage;
using Wr.UmbracoCampaignPhoneManager.Tests.Providers.Storage;
using static Wr.UmbracoCampaignPhoneManager.XmlHelper;

namespace Wr.UmbracoCampaignPhoneManager.Tests.Criteria
{
    [TestClass]
    public class ReferrerCriteriaTest
    {
        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNoReferrer_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNoMatchingReferrer_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", Referrer="google.co.uk" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "bing.com" }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithExtactMatchingReferrer_ReturnsCorrectResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", Referrer = "google.co.uk" },
                new CampaignDetail() { Id = "1202", TelephoneNumber = "9909 999 9999", CampaignCode = "testcode2", Referrer = "google.com" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "google.co.uk" }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().Id, dataModel.CampaignDetail.First().Id);
        }

        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithTopTierDomainMatchingReferrer_ReturnsCorrectResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", Referrer = "google.co.uk" },
                new CampaignDetail() { Id = "1202", TelephoneNumber = "9909 999 9999", CampaignCode = "testcode2", Referrer = "google.com" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "www.google.co.uk" }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().Id, dataModel.CampaignDetail.First().Id);
        }

        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithTopTierDomainAndSubDomainMatchingReferrer_ReturnsCorrectResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", Referrer = "google.co.uk" },
                new CampaignDetail() { Id = "1202", TelephoneNumber = "9909 999 9999", CampaignCode = "testcode2", Referrer = "google.com" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "www.search.google.co.uk" }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().Id, dataModel.CampaignDetail.First().Id);
        }

        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithTopTierDomainAndSubDomainNotMatchingReferrer_ReturnsNoResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", Referrer = "www.google.co.uk" },
                new CampaignDetail() { Id = "1202", TelephoneNumber = "9909 999 9999", CampaignCode = "testcode2", Referrer = "google.com" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "www.search.google.co.uk" }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void ReferrerCriteria_GetMatchingRecordsFromPhoneManagerTest_WithTopTierDomainAndSubDomainWithMatchingReferrerAndCloseMatch_ReturnsCorrectResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode", Referrer = "www.google.co.uk" },
                new CampaignDetail() { Id = "1202", TelephoneNumber = "9909 999 9999", CampaignCode = "testcode2", Referrer = "google.com" },
                new CampaignDetail() { Id = "1203", TelephoneNumber = "8888 888 8888", CampaignCode = "testcode3", Referrer = "google.co.uk" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "www.search.google.co.uk" }
            };

            var criteria = new ReferrerCriteria();

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager(criteriaParameters, _repository);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().Id, "1203");
        }
    }
}
