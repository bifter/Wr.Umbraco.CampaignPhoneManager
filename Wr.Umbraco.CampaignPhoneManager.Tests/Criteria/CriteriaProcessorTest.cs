using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Criteria;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers.Storage;
using static Wr.Umbraco.CampaignPhoneManager.XmlHelper;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Criteria
{
    [TestClass]
    class CriteriaProcessorTest
    { 
        [TestMethod]
        public void CriteriaProcessor_GetMatchingRecordsFromPhoneManagerTest_WithNotData_ReturnsEmptyResults()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);

            IRepository _repository = TestRepository.GetRepository(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfo_NotIncludingQueryStrings = new CampaignDetail() { }
            };

            var criteria = new CriteriaProcessor(criteriaParameters, _repository);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
        }
    }
}
