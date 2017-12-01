using Moq;
using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Data;

namespace Wr.UmbracoPhoneManager.Tests
{
    public class DefaultMockData
    {
        public static Mock<IDataProvider> MockDataProvider(List<PhoneNumber> phoneNumbers = null, DefaultSettings defaultSettings = null)
        {
            var mock = new Mock<IDataProvider>();

            if (phoneNumbers != null)
            {
                mock.Setup(x => x.GetAllCampaignCodeRecords()).Returns(phoneNumbers);
            }
            else
            {
                mock.Setup(x => x.GetAllCampaignCodeRecords()).Returns(mockPhoneNumbers);
            }

            if (defaultSettings != null)
            {
                mock.Setup(x => x.GetDefaultSettings()).Returns(defaultSettings);
            }
            else
            {
                mock.Setup(x => x.GetDefaultSettings()).Returns(mockDefaultSettings);
            }
            return mock;
        }

        public static List<PhoneNumber> mockPhoneNumbers =>
            new List<PhoneNumber>()
            {
                new PhoneNumber() { phoneNumber = MockConstants.MockTestPhoneNumberData.PhoneNumber, campaignCode = MockConstants.MockTestPhoneNumberData.CampaignCode },
                new PhoneNumber() { phoneNumber = "9999999999", campaignCode = "dummy" }
            };

        /// <summary>
        /// Default Admin Settings
        /// </summary>
        public static DefaultSettings mockDefaultSettings =>
            new DefaultSettings()
            {
                DefaultCampaignQuerystringKey = MockConstants.DefaultData.DefaultCampaignQuerystringKey,
                DefaultPhoneNumber = MockConstants.DefaultData.DefaultPhoneNumber,
                DefaultPersistDurationInDays = MockConstants.DefaultData.DefaultPersistDurationInDays
            };
           
        

    }
}
