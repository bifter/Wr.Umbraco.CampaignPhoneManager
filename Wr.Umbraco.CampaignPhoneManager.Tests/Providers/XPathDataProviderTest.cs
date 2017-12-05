using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Providers
{
    [TestClass]
    public class XPathDataProviderTest
    {
        [TestMethod]
        public void XPathDataProvider_LoadDefaultSettings_WithAllProperties_ReturnValid()
        {
            // Arrange
            var defaultSettings = new DefaultSettings() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { phoneNumber = "0800 123 4567", campaignCode = "testcode"}};

            var testPhoneManagerData = GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_TestXml_OnlyForUnitTesting(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(act.DefaultPhoneNumber == defaultSettings.DefaultPhoneNumber);
            Assert.IsTrue(act.DefaultCampaignQueryStringKey == defaultSettings.DefaultCampaignQueryStringKey);
            Assert.IsTrue(act.DefaultPersistDurationInDays == defaultSettings.DefaultPersistDurationInDays);
        }

        [TestMethod]
        public void XPathDataProvider_LoadDefaultSettings_WithDefaultValueForMissingPropertyDefaultPersistDurationInDays_ReturnValid()
        {
            // Arrange
            var defaultSettings = new DefaultSettings() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { phoneNumber = "0800 123 4567", campaignCode = "testcode" } };

            var testPhoneManagerData = GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_TestXml_OnlyForUnitTesting(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(act.DefaultPhoneNumber == defaultSettings.DefaultPhoneNumber);
            Assert.IsTrue(act.DefaultCampaignQueryStringKey == defaultSettings.DefaultCampaignQueryStringKey);
            Assert.IsTrue(act.DefaultPersistDurationInDays == 30);
        }

        [TestMethod]
        public void XPathDataProvider_LoadDefaultSettings_WithEmptyDefaultPhoneNumber_ReturnValid()
        {
            // Arrange
            var defaultSettings = new DefaultSettings() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { phoneNumber = "0800 123 4567", campaignCode = "testcode" } };

            var testPhoneManagerData = GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_TestXml_OnlyForUnitTesting(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(act.DefaultPhoneNumber));
        }

        [TestMethod]
        public void XPathDataProvider_LoadDefaultSettings_WithMissingDefaultPhoneNumber_ReturnValid()
        {
            // Arrange
            var defaultSettings = new DefaultSettings() { DefaultPhoneNumber = null, DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { phoneNumber = "0800 123 4567", campaignCode = "testcode" } };

            var testPhoneManagerData = GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_TestXml_OnlyForUnitTesting(testPhoneManagerData));

            // Act
            var act = method.GetDefaultSettings();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(act.DefaultPhoneNumber));
        }


        [TestMethod]
        public void XPathDataProvider_GetMatchingRecordFromPhoneManager_WithNoRequestParameters_ReturnEmptyModel()
        {
            // Arrange
            var defaultSettings = new DefaultSettings() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>() { new CampaignDetail() { phoneNumber = "0800 123 4567", campaignCode = "testcode" } };

            var testPhoneManagerData = GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_TestXml_OnlyForUnitTesting(testPhoneManagerData));

            var requestInfo = new CampaignDetail();
            var queryStrings = new NameValueCollection();

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.IsNull(foundPhoneNumber.id);
        }

        [TestMethod]
        public void XPathDataProvider_GetMatchingRecordFromPhoneManager_WithValidDefaultSettingQueryStringValue_ReturnValidModel()
        {
            // Arrange
            var defaultSettings = new DefaultSettings() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 0 };
            List<CampaignDetail> phoneNumbers = new List<CampaignDetail>()
            {
                new CampaignDetail() { phoneNumber = "0800 123 4567", campaignCode = "testcode" },
                new CampaignDetail() { phoneNumber = "0800 222 2222", campaignCode = "dummy" }
            };

            var testPhoneManagerData = GeneratePhoneManagerTestDataString(defaultSettings, phoneNumbers);

            var method = new XPathDataProvider(new XPathDataProviderSource_TestXml_OnlyForUnitTesting(testPhoneManagerData));

            var requestInfo = new CampaignDetail();
            var queryStrings = new NameValueCollection();
            queryStrings.Add("fsource", "testcode");

            // Act
            var foundPhoneNumber = method.GetMatchingRecordFromPhoneManager(requestInfo, queryStrings);

            // Assert
            Assert.IsNotNull(foundPhoneNumber);
            Assert.AreEqual(foundPhoneNumber.id, "1201");
            Assert.AreEqual(foundPhoneNumber.phoneNumber, "0800 123 4567");
        }


        public string GeneratePhoneManagerTestDataString(DefaultSettings defaultSettings, List<CampaignDetail> phoneNumbers)
        {
            string phoneManager_Start = "<campaignPhoneManager id=\"1152\" key=\"ee64b6fe-21dc-445c-9256-1d5497f91383\" parentID=\"1103\" level=\"2\" creatorID=\"0\" sortOrder=\"5\" createDate=\"2017-11-13T12:20:43\" updateDate=\"2017-11-27T17:38:26\" nodeName=\"Phone manager\" urlName=\"phone-manager\" path=\"-1,1103,1152\" isDoc=\"\" nodeType=\"1151\" creatorName=\"Joe Bloggs\" writerName=\"Joe Bloggs\" writerID=\"0\" template=\"0\" nodeTypeAlias=\"phoneManager\">";
            string phoneManager_End = "</campaignPhoneManager>";
            string phoneNumber_Start = "<campaignDetail id=\"{0}\" key=\"8b0d2b79-f219-47c0-9c44-a6dc9620{0}\" parentID=\"1152\" level=\"3\" creatorID=\"0\" sortOrder=\"0\" createDate=\"2017-11-27T17:37:57\" updateDate=\"2017-11-27T17:37:57\" nodeName=\"Test number\" urlName=\"test-number\" path=\"-1,1103,1152,1201\" isDoc=\"\" nodeType=\"1150\" creatorName=\"Joe Bloggs\" writerName=\"Joe Bloggs\" writerID=\"0\" template=\"0\" nodeTypeAlias=\"phoneNumber\">";
            string phoneNumber_End = "</campaignDetail>";
            int startingId = 1201;

            if (defaultSettings != null && phoneNumbers != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(phoneManager_Start);

                // Add default settings - use null to not show element at all
                sb.Append(FormatProperty(defaultSettings.DefaultPhoneNumber, "<defaultPhoneNumber>{0}</defaultPhoneNumber>"));
                sb.Append(FormatProperty(defaultSettings.DefaultCampaignQueryStringKey, "<defaultCampaignQueryStringKey>{0}</defaultCampaignQueryStringKey>"));
                sb.Append(FormatProperty(defaultSettings.DefaultPersistDurationInDays, "<defaultPersistDurationInDays>{0}</defaultPersistDurationInDays>"));

                foreach (var item in phoneNumbers)
                {
                    sb.AppendFormat(phoneNumber_Start, startingId.ToString());

                    // Add phoneNumber properties
                    sb.Append(FormatProperty(item.phoneNumber, "<phoneNumber>{0}</phoneNumber>"));
                    sb.Append(FormatProperty(item.doNotPersistAcrossVisits, "<doNotPersistAcrossVisits>{0}</doNotPersistAcrossVisits>"));
                    sb.Append(FormatProperty(item.persistDurationOverride, "<persistDurationOverride>{0}</persistDurationOverride>"));
                    sb.Append(FormatProperty(item.referrer, "<referrer>{0}</referrer>"));
                    sb.Append(FormatProperty(item.campaignCode, "<campaignCode>{0}</campaignCode>"));
                    sb.Append(FormatProperty(item.entryPage, "<entryPage>{0}</entryPage>"));
                    sb.Append(FormatProperty(item.overwritePersistingItem, "<overwritePersistingItem>{0}</overwritePersistingItem>"));
                    sb.Append(FormatProperty(item.altMarketingCode, "<altMarketingCode>{0}</altMarketingCode>"));
                    sb.Append(FormatProperty(item.priorityOrder, "<priorityOrder>{0}</priorityOrder>"));
                    sb.Append(FormatProperty(item.useAltCampaignQueryStringKey, "<useAltCampaignQueryStringKey>{0}</useAltCampaignQueryStringKey>"));

                    sb.Append(phoneNumber_End);
                    startingId++; // increment phoneNumber id
                }
                sb.Append(phoneManager_End);

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Format the property automatically
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyValue"></param>
        /// <param name="holder"></param>
        /// <returns></returns>
        private string FormatProperty<T>(T propertyValue, string holder)
        {
            if (propertyValue != null)
            {
                if (propertyValue.GetType() == typeof(string))
                {
                    string cDataHolder = "<![CDATA[{0}]]>";
                    var value  = propertyValue.ToString();
                    if (value != string.Empty) // if there is a value then enclose it in the CDATA holder
                        value = string.Format(cDataHolder, value);

                    return string.Format(holder, value);

                }
                return string.Format(holder, propertyValue);
            }
            return string.Empty;
        }
    }
}
