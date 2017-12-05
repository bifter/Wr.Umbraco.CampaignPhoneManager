using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Tests
{
    [TestClass]
    public class CampaignPhoneManagerTest
    {
        // Without Existing cookie

        /*
        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithNoCookie_WithNoFoundPhoneNumber_WithNoDefaultPhonenumber_ReturnsOutputResultSourceLastResortPlaceholder_ReturnsNoCookie()
        {
            // Arrange
            CookieHolder mockExisingCookieHolder = null;
            PhoneNumber mockPhoneNumber = null;
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            defaultSetting.DefaultPhoneNumber = string.Empty; // ie. no default admin phone number set

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.LastResortPlaceholder);
            Assert.AreEqual(OutputModelResult.PhoneNumber, MockConstants.DefaultData.LastResortPlaceholder);
            Assert.IsTrue(TestHelpers.CheckIfNullOrEmpty(OutputModelResult.CampaignCode));
            Assert.IsTrue(TestHelpers.CheckIfNullOrEmpty(OutputModelResult.AltMarketingCode));
            Assert.IsNull(OutputCookieHolder);
        }


        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithNoCookie_WithNoFoundPhoneNumber_ReturnsOutputResultSourceDefaultNumberFromAdmin_ReturnsNoCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = null;
            PhoneNumber mockPhoneNumber = null;

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.DefaultNumberFromAdmin);
            Assert.AreEqual(OutputModelResult.PhoneNumber, MockConstants.DefaultData.DefaultPhoneNumber);
            Assert.IsTrue(TestHelpers.CheckIfNullOrEmpty(OutputModelResult.CampaignCode));
            Assert.IsTrue(TestHelpers.CheckIfNullOrEmpty(criteria.OutputModelResult.AltMarketingCode));
            Assert.IsNull(OutputCookieHolder);
        }

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithNoCookie_WithFoundPhoneNumber_WithPersist_ReturnsOutputResultSourceFoundRecordFromCriteria_ReturnsCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = null;
            PhoneNumber mockPhoneNumber = MockTestPhonenumber;
            mockPhoneNumber.doNotPersist = false; // i.e. persist!

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.FoundRecordFromCriteria);

            Assert.AreEqual(OutputModelResult.PhoneNumber, mockPhoneNumber.phoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, mockPhoneNumber.campaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, mockPhoneNumber.altMarketingCode);

            Assert.IsNotNull(OutputCookieHolder);
            Assert.AreEqual(OutputCookieHolder.Expires, DateTime.Today.AddDays(MockConstants.DefaultData.DefaultPersistDurationInDays));
            Assert.AreEqual(OutputCookieHolder.Model.PhoneNumber, MockTestPhonenumber.phoneNumber);
            Assert.AreEqual(OutputCookieHolder.Model.CampaignCode, MockTestPhonenumber.campaignCode);
            Assert.AreEqual(OutputCookieHolder.Model.AltMarketingCode, MockTestPhonenumber.altMarketingCode);
        }

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithNoCookie_WithFoundPhoneNumber_WithPersist_WithPersistDurationOveride_ReturnsOutputResultSourceFoundRecordFromCriteria_ReturnsCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = null;
            PhoneNumber mockPhoneNumber = MockTestPhonenumber;
            mockPhoneNumber.doNotPersist = false; // i.e. persist!
            mockPhoneNumber.persistDurationOverride = 60; // override default persist duration in days

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.FoundRecordFromCriteria);

            Assert.AreEqual(OutputModelResult.PhoneNumber, mockPhoneNumber.phoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, mockPhoneNumber.campaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, mockPhoneNumber.altMarketingCode);

            Assert.IsNotNull(OutputCookieHolder);
            Assert.AreEqual(OutputCookieHolder.Expires, DateTime.Today.AddDays(mockPhoneNumber.persistDurationOverride));
            Assert.AreEqual(OutputCookieHolder.Model.PhoneNumber, MockTestPhonenumber.phoneNumber);
            Assert.AreEqual(OutputCookieHolder.Model.CampaignCode, MockTestPhonenumber.campaignCode);
            Assert.AreEqual(OutputCookieHolder.Model.AltMarketingCode, MockTestPhonenumber.altMarketingCode);
        }

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithNoCookie_WithFoundPhoneNumber_WithOutPersist_ReturnsOutputResultSourceFoundRecordFromCriteria_ReturnsNoCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = null;
            PhoneNumber mockPhoneNumber = MockTestPhonenumber;
            mockPhoneNumber.doNotPersist = true; // i.e. do not persist!

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.FoundRecordFromCriteria);

            Assert.AreEqual(OutputModelResult.PhoneNumber, mockPhoneNumber.phoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, mockPhoneNumber.campaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, mockPhoneNumber.altMarketingCode);

            Assert.IsNull(OutputCookieHolder);
        }


        // With Existing cookie

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithExistingCookie_WithNoFoundPhoneNumber_ReturnsOutputResultSourceExisitingCookie_ReturnsNoCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = MockTestCookieData;
            PhoneNumber mockPhoneNumber = null;

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.ExisitingCookie);

            Assert.AreEqual(OutputModelResult.PhoneNumber, MockConstants.MockTestCookieData.PhoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, MockConstants.MockTestCookieData.CampaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, MockConstants.MockTestCookieData.AltMarketingCode);

            Assert.IsNull(criteria.OutputCookieHolder);
        }

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithExistingCookie_WithFoundPhoneNumber_WithPersist_WithNoPersistOverride_ReturnsOutputResultSourceFoundRecordFromCriteria_ReturnsNoCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = MockTestCookieData;
            PhoneNumber mockPhoneNumber = MockTestPhonenumber;
            mockPhoneNumber.doNotPersist = false; // i.e. persist!, but no persist override

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.ExisitingCookie);

            Assert.AreEqual(OutputModelResult.PhoneNumber, mockExisingCookieHolder.Model.PhoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, mockExisingCookieHolder.Model.CampaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, mockExisingCookieHolder.Model.AltMarketingCode);

            Assert.IsNull(OutputCookieHolder);
        }

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithExistingCookie_WithFoundPhoneNumber_WithPersist_WithPersistOverride_ReturnsOutputResultSourceFoundRecordFromCriteria_ReturnsCookie()
        {

            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = MockTestCookieData;
            PhoneNumber mockPhoneNumber = MockTestPhonenumber;
            mockPhoneNumber.doNotPersist = false; // i.e. persist!
            mockPhoneNumber.overwritePersistingNumber = true;

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.FoundRecordFromCriteria);

            Assert.AreEqual(OutputModelResult.PhoneNumber, mockPhoneNumber.phoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, mockPhoneNumber.campaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, mockPhoneNumber.altMarketingCode);

            Assert.IsNotNull(OutputCookieHolder);
            Assert.AreEqual(OutputCookieHolder.Model.PhoneNumber, mockPhoneNumber.phoneNumber);
            Assert.AreEqual(OutputCookieHolder.Model.CampaignCode, mockPhoneNumber.campaignCode);
            Assert.AreEqual(OutputCookieHolder.Model.AltMarketingCode, mockPhoneNumber.altMarketingCode);
        }

        [TestMethod]
        public void AppDataProcessor_OutputResultSource_WithExistingCookie_WithFoundPhoneNumber_WithOutPersist_ReturnsOutputResultSourceExisitingCookie()
        {
            // Arrange
            var defaultSetting = DefaultMockData.mockDefaultSettings;
            CookieHolder mockExisingCookieHolder = MockTestCookieData;
            PhoneNumber mockPhoneNumber = MockTestPhonenumber;
            mockPhoneNumber.doNotPersist = true; // i.e. do not persist!

            var criteria = new AppDataProcessor(defaultSetting, mockExisingCookieHolder, mockPhoneNumber);

            //Act
            var OutputResultSource = criteria.OutputResultSource;
            var OutputModelResult = criteria.OutputModelResult;
            var OutputCookieHolder = criteria.OutputCookieHolder;

            // Assert
            Assert.IsTrue(OutputResultSource == AppDataProcessor.OutputSource.ExisitingCookie);
            Assert.AreEqual(OutputModelResult.PhoneNumber, MockConstants.MockTestCookieData.PhoneNumber);
            Assert.AreEqual(OutputModelResult.CampaignCode, MockConstants.MockTestCookieData.CampaignCode);
            Assert.AreEqual(OutputModelResult.AltMarketingCode, MockConstants.MockTestCookieData.AltMarketingCode);
            Assert.IsNull(OutputCookieHolder);
        }


        #region Mocks

        private CookieHolder MockTestCookieData =>
            new CookieHolder()
            {
                Expires = MockConstants.MockTestCookieData.Expires,
                Model = new OutputModel()
                {
                     PhoneNumber = MockConstants.MockTestCookieData.PhoneNumber,
                     CampaignCode = MockConstants.MockTestCookieData.CampaignCode,
                     AltMarketingCode = MockConstants.MockTestCookieData.AltMarketingCode
                }
            };

        private PhoneNumber MockTestPhonenumber =>
            new PhoneNumber()
            {
                phoneNumber = "111 111",
                campaignCode = "CCCCCCC",
                altMarketingCode = "altaltalt"
            };

        #endregion
        */

    }
}
