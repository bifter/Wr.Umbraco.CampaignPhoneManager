using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wr.Umbraco.CampaignPhoneManager.Criteria;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers;
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
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);

            IDataProvider _dataSource = new XPathDataProviderSource_GetXPathNavigatorMock(testPhoneManagerData);

            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = null,
                RequestInfoNotIncludingQueryStrings = new CampaignDetail() { }
            };

            var criteria = new CriteriaProcessor(criteriaParameters, _dataSource);

            // Act
            var results = criteria.GetMatchingRecordFromPhoneManager();

            // Assert
            Assert.IsNotNull(results);
        }
    }
}
