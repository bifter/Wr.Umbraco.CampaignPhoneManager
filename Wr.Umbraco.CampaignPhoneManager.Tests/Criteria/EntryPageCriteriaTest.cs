using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Criteria;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers;
using static Wr.Umbraco.CampaignPhoneManager.XmlHelper;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Criteria
{
    [TestClass]
    public class EntryPageCriteriaTest
    {
        [TestMethod]
        public void EntryPageCriteria_GetMatchingRecordsFromPhoneManagerTest_WithNotEntryPage_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);

            IXPathDataProviderSource _dataSource = new XPathDataProviderSource_GetXPathNavigatorMock(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfoNotIncludingQueryStrings = new CampaignDetail() {}
            };

            var criteria = new EntryPageCriteria_DataSource_XPath(criteriaParameters, _dataSource);

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void EntryPageCriteria_GetMatchingRecordsFromPhoneManagerTest_WithMatchingEntryPage_ReturnsMatchingResult()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", PhoneNumber = "0800 123 4567", CampaignCode = "testcode", EntryPage="Homepage" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);

            IXPathDataProviderSource _dataSource = new XPathDataProviderSource_GetXPathNavigatorMock(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfoNotIncludingQueryStrings = new CampaignDetail() { EntryPage = dataModel.CampaignDetail.First().EntryPage }
            };

            var criteria = new EntryPageCriteria_DataSource_XPath(criteriaParameters, _dataSource);

            // Act
            var results = criteria.GetMatchingRecordsFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().EntryPage, dataModel.CampaignDetail.First().EntryPage);
        }
    }
}
