using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers
{
    [TestClass]
    public class XPathDataProviderTest_GetXPathNavigatorTest
    {
        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_LoadDefaultSettings_WithAllProperties_ReturnValid()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(act.DefaultPhoneNumber == defaultSettings.DefaultPhoneNumber);
            Assert.IsTrue(act.DefaultCampaignQueryStringKey == defaultSettings.DefaultCampaignQueryStringKey);
            Assert.IsTrue(act.DefaultPersistDurationInDays == defaultSettings.DefaultPersistDurationInDays);
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_LoadDefaultSettings_WithDefaultValueForMissingPropertyDefaultPersistDurationInDays_ReturnValid()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(act.DefaultPhoneNumber == defaultSettings.DefaultPhoneNumber);
            Assert.IsTrue(act.DefaultCampaignQueryStringKey == defaultSettings.DefaultCampaignQueryStringKey);
            Assert.IsTrue(act.DefaultPersistDurationInDays == 30);
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_LoadDefaultSettings_WithEmptyDefaultPhoneNumber_ReturnValid()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(act.DefaultPhoneNumber));
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_LoadDefaultSettings_WithMissingDefaultPhoneNumber_ReturnValid()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = null, DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(act.DefaultPhoneNumber));
        }


        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithNoRequestParameters_ReturnEmptyModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail();
            var queryStrings = new NameValueCollection();

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNull(foundPhoneNumber);
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidDefaultSettingQueryStringValue_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" },
                new CampaignDetail() { PhoneNumber = "0800 222 2222", CampaignCode = "dummy" }
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail();
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1201");
            Assert.AreEqual(foundPhoneNumber.PhoneNumber, "0800 123 4567");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidDefaultSettingQueryStringValue_TestWithValidUseAltCampaignQueryStringKey_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "testcode" },
                new CampaignDetail() { PhoneNumber = "0800 222 2222", CampaignCode = "dummy" },
                new CampaignDetail() { PhoneNumber = "0800 222 4444", CampaignCode = "testcode", UseAltCampaignQueryStringKey="asdasd" }
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail();
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1201");
            Assert.AreEqual(foundPhoneNumber.PhoneNumber, "0800 123 4567");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidUseAltCampaignQueryStringKeyQueryStringValue_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode", UseAltCampaignQueryStringKey="altqskey" },
                new CampaignDetail() { PhoneNumber = "0800 222 2222", CampaignCode = "dummy" },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail();
            var queryStrings = new NameValueCollection();
            queryStrings.Add("altqskey", "altcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1201");
            Assert.AreEqual(foundPhoneNumber.PhoneNumber, "0800 123 4567");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidExactReferrerMatch_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 222 2222", CampaignCode = "dummy" },
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode", Referrer="www.mydomain.com" },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail() { Referrer = "www.mydomain.com" };
            var queryStrings = new NameValueCollection();

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1202");
            Assert.AreEqual(foundPhoneNumber.PhoneNumber, "0800 123 4567");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidPartialReferrerMatch_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode", Referrer="mydomain.com" },
                new CampaignDetail() { PhoneNumber = "0800 222 2222", CampaignCode = "dummy" },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail() { Referrer = "www.mydomain.com" };
            var queryStrings = new NameValueCollection();

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1201");
            Assert.AreEqual(foundPhoneNumber.PhoneNumber, "0800 123 4567");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithMultipleValidMatch_ReturnExactModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode" },
                new CampaignDetail() { PhoneNumber = "0800 222 6666", CampaignCode = "testcode" },
                new CampaignDetail() { PhoneNumber = "0800 222 2222", CampaignCode = "testcode", Referrer="mydomain.com" },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail() { Referrer = "www.mydomain.com" };
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1203");
            Assert.AreEqual(foundPhoneNumber.PhoneNumber, "0800 222 2222");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithNonValidEntryPage_ReturnNoModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode" },
                new CampaignDetail() { PhoneNumber = "0800 222 6666", CampaignCode = "dummy" },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail() { Referrer = "www.mydomain.com", EntryPage = "home" };
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNull(foundPhoneNumber);
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidEntryPage_ReturnNoModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode" },
                new CampaignDetail() { PhoneNumber = "0800 222 6666", CampaignCode = "dummy", EntryPage = "home" },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail() { Referrer = "www.mydomain.com", EntryPage = "home" };
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1202");
        }

        [TestMethod]
        public void XPathDataProvider_GetXPathNavigator_GetMatchingRecordFromPhoneManager_WithValidEntryPage_WithValidReferrer_TestPriorityOrder_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { PhoneNumber = "0800 123 4567", CampaignCode = "altcode", Referrer = "mydomain.com", PriorityOrder=10 },
                new CampaignDetail() { PhoneNumber = "0800 222 6666", CampaignCode = "dummy", EntryPage = "home", PriorityOrder=11 },
            };

            var testPhoneManagerData = DataHelpers.GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            var requestInfo = new CampaignDetail() { Referrer = "www.mydomain.com", EntryPage = "home" };
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.Id, "1202");
        }
    }
}
