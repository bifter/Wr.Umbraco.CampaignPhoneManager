using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Criteria;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers.Storage;
using static Wr.Umbraco.CampaignPhoneManager.XmlHelper;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Criteria
{
    [TestClass]
    public class CriteriaProcessorTest
    { 
        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecordsFromPhoneManagerTest_WithNoData_ReturnsEmptyResults()
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

            var criteria = new CriteriaProcessor(criteriaParameters, _repository);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNull(results);
        }

        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecordsFromPhoneManagerTest_WithMatchingData_CampaignCode_ReturnsValidResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "thisisrubbish" },
                new CampaignDetail() { Id = "1202", TelephoneNumber = "0800 000 0000", CampaignCode = "testcode" },
                new CampaignDetail() { Id = "1203", TelephoneNumber = "9999 999 9999", CampaignCode = "dummy" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "fsource", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { }
            };

            var criteria = new CriteriaProcessor(criteriaParameters, _repository);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Id, "1202");
        }

        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecordsFromPhoneManagerTest_WithMatchingData_Referrer_ReturnsValidResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "thisisrubbish", Referrer="sub.domain.co.uk" },
                new CampaignDetail() { Id = "1302", TelephoneNumber = "0800 000 0000", CampaignCode = "dummy", Referrer="domain.co.uk" },
                new CampaignDetail() { Id = "1203", TelephoneNumber = "9999 999 9999", CampaignCode = "dummy2" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "fsource", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "www.domain.co.uk" }
            };

            var criteria = new CriteriaProcessor(criteriaParameters, _repository);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Id, "1302");
        }

        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecordsFromPhoneManagerTest_WithMatchingData_EntryPage_ReturnsValidResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "thisisrubbish", Referrer="sub.domain.co.uk" },
                new CampaignDetail() { Id = "1302", TelephoneNumber = "0800 000 0000", CampaignCode = "dummy", Referrer="domain.co.uk" },
                new CampaignDetail() { Id = "1203", TelephoneNumber = "9999 999 9999", CampaignCode = "dummy2", EntryPage = "AboutUs" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { },
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { EntryPage = "AboutUs" }
            };

            var criteria = new CriteriaProcessor(criteriaParameters, _repository);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Id, "1203");
        }

        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecordsFromPhoneManagerTest_WithMatchingData_EntryPageAndCampaignCodeAndReferrer_ReturnsValidResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>()
            {
                new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "thisisrubbish", Referrer="sub.domain.co.uk" },
                new CampaignDetail() { Id = "1302", TelephoneNumber = "0800 000 0000", CampaignCode = "dummy", Referrer="domain.co.uk" },
                new CampaignDetail() { Id = "1203", TelephoneNumber = "9999 999 9999", CampaignCode = "dummy2", EntryPage = "AboutUs" },
                new CampaignDetail() { Id = "1204", TelephoneNumber = "8888 888 8888", CampaignCode = "testcode", EntryPage = "AboutUs" },
                new CampaignDetail() { Id = "1205", TelephoneNumber = "7777 777 7777", CampaignCode = "testcode", EntryPage = "AboutUs", Referrer="domain.co.uk" }
            };
            var testPhoneManagerData = dataModel.ToXmlString();

            IRepository _repository = MockProviders.Repository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = new NameValueCollection() { { "fsource", "testcode" } },
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { Referrer = "www.domain.co.uk", EntryPage = "AboutUs" }
            };

            var criteria = new CriteriaProcessor(criteriaParameters, _repository);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Id, "1205");
        }
    }
}
