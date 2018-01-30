using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Tests
{
    [TestFixture]
    public class PhoneManagerAppTest
    {
        [Test]
        public void PhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithNoCookie_WithNoFoundPhoneNumber_WithNoDefaultPhonenumber_ReturnsLastResortPhoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            var _dataProvider = MockProviders.Repository(testPhoneManagerData);

            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = null,
                OutputModelResult = new OutputModel() { TelephoneNumber = AppConstants.LastResortPhoneNumberPlaceholder },
                OutputResultSource = OutputSource.LastResortPlaceholder
            };

            PhoneManager target = new PhoneManager(null, _dataProvider, null, null, null, null);
            //PrivateObject obj = new PrivateObject(target); // MS Test

            //Act
            FinalResultModel retVal = target.ProcessAllPotentialCandidatePhoneNumbers(new CookieHolder(), new PhoneManagerCampaignDetail());
            //FinalResultModel retVal = (FinalResultModel)obj.Invoke("ProcessAllPotentialCandidatePhoneNumbers", new object[] { new CookieHolder(), new PhoneManagerCampaignDetail() }); // MS Test

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
            Assert.AreEqual(retVal.OutputModelResult.TelephoneNumber, correctResult.OutputModelResult.TelephoneNumber);
        }

        [Test]
        public void PhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithNoCookie_WithFoundPhoneNumber_ReturnsFoundPhoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            var _dataProvider = MockProviders.Repository(testPhoneManagerData);

            var foundRecord = new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "FOUND PHONENUMBER" };
            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = null,
                OutputModelResult = new OutputModel() { TelephoneNumber = "FOUND PHONENUMBER" },
                OutputResultSource = OutputSource.FoundRecordFromCriteria
            };

            PhoneManager target = new PhoneManager(null, _dataProvider, null, null, null, null);
            
            //Act
            FinalResultModel retVal = target.ProcessAllPotentialCandidatePhoneNumbers(new CookieHolder(), foundRecord );

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
            Assert.AreEqual(retVal.OutputModelResult.TelephoneNumber, correctResult.OutputModelResult.TelephoneNumber);
        }

        [Test]
        public void PhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithCookie_WithFoundPhoneNumberNoPersist_ReturnsCookie()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            var _dataProvider = MockProviders.Repository(testPhoneManagerData);

            var foundRecord = new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "FOUND PHONENUMBER" };

            var _cookie = new CookieHolder() { Model =
                new OutputModel()
                {
                    Id = "1202",
                    TelephoneNumber = "9999 999 9999"
                }
            };

            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = new CookieHolder(),
                OutputModelResult = new OutputModel() {
                    Id = "1202",
                    TelephoneNumber = "9999 999 9999"
                },
                OutputResultSource = OutputSource.ExisitingCookie
            };

            PhoneManager target = new PhoneManager(null, _dataProvider, null, null, null, null);

            //Act
            FinalResultModel retVal = target.ProcessAllPotentialCandidatePhoneNumbers(_cookie, foundRecord);

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
            Assert.AreEqual(retVal.OutputModelResult.Id, correctResult.OutputModelResult.Id);
        }

        [Test]
        public void PhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithCookie_WithFoundPhoneNumberWithPersist_ReturnsFoundRecordAndSetCookie()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail = new List<PhoneManagerCampaignDetail>() { new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = dataModel.ToXmlString();

            var _dataProvider = MockProviders.Repository(testPhoneManagerData);

            var foundRecord = new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", OverwritePersistingItem = true };

            var _cookie = new CookieHolder()
            {
                Model =
                new OutputModel()
                {
                    Id = "1202",
                    TelephoneNumber = "9999 999 9999"
                }
            };

            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = new CookieHolder() { Model = 
                    new OutputModel()
                    {
                        Id = "1201",
                        TelephoneNumber = "0800 123 4567"
                    }
                },
                OutputModelResult = new OutputModel()
                {
                    Id = "1201",
                    TelephoneNumber = "0800 123 4567"
                },
                OutputResultSource = OutputSource.FoundRecordFromCriteria
            };

            PhoneManager target = new PhoneManager(null, _dataProvider, null, null, null, null);

            //Act
            FinalResultModel retVal = target.ProcessAllPotentialCandidatePhoneNumbers(_cookie, foundRecord);

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
            Assert.AreEqual(retVal.OutputModelResult.Id, correctResult.OutputModelResult.Id);
            Assert.AreEqual(retVal.OutputCookieHolder.Model.Id, correctResult.OutputCookieHolder.Model.Id);
        }

        [Test]
        public void PhoneManagerApp_ProcessRequest_WithNoInputsNoDefaultTelephoneNumber_ReturnsLastResortPhoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel();
            dataModel.PhoneManagerCampaignDetail =
                   new List<PhoneManagerCampaignDetail>()
                   {
                       new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" },
                       new PhoneManagerCampaignDetail() { Id = "1202", TelephoneNumber = "0800 000 1111", CampaignCode = "testcode2" }
                   };
            var testPhoneManagerData = dataModel.ToXmlString();

            var AppParamHolder = new MockProviders.PhoneManagerAppParamHolder
                (
                    new CookieHolder(), // cookie
                    testPhoneManagerData, // repository data
                    new NameValueCollection(), // querystring
                    "", // referrer
                    new OutputModel(), // session
                    "" // umbraco current page id
                );

            // generate the required result

            PhoneManager app = new PhoneManager(AppParamHolder.CookieProvider, AppParamHolder.RepositoryProvider, AppParamHolder.QueryStringProvider, AppParamHolder.ReferrerProvider, AppParamHolder.SessionProvider, AppParamHolder.UmbracoProvider);

            //Act
            OutputModel actualResult = app.ProcessRequest();

            //Assert
            Assert.IsNotNull(actualResult.Id);
            Assert.AreEqual(AppConstants.LastResortPhoneNumberPlaceholder, actualResult.TelephoneNumber);
        }

        [Test]
        public void PhoneManagerApp_ProcessRequest_WithNoInputsWithDefaultTelephoneNumber_ReturnsDefaultTelephoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail =
                   new List<PhoneManagerCampaignDetail>()
                   {
                       new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" },
                       new PhoneManagerCampaignDetail() { Id = "1202", TelephoneNumber = "0800 000 1111", CampaignCode = "testcode2" },
                       new PhoneManagerCampaignDetail() { Id = "1203", TelephoneNumber = "0800 999 9999", IsDefault = true }
                   };
            var testPhoneManagerData = dataModel.ToXmlString();

            var AppParamHolder = new MockProviders.PhoneManagerAppParamHolder
                (
                    new CookieHolder(), // cookie
                    testPhoneManagerData, // repository data
                    new NameValueCollection(), // querystring
                    "", // referrer
                    new OutputModel(), // session
                    "" // umbraco current page id
                );

            // generate the required result

            PhoneManager app = new PhoneManager(AppParamHolder.CookieProvider, AppParamHolder.RepositoryProvider, AppParamHolder.QueryStringProvider, AppParamHolder.ReferrerProvider, AppParamHolder.SessionProvider, AppParamHolder.UmbracoProvider);

            //Act
            OutputModel actualResult = app.ProcessRequest();

            //Assert
            Assert.IsNotNull(actualResult.Id);
            Assert.AreEqual("1203", actualResult.Id);
        }

        [Test]
        public void PhoneManagerApp_ProcessRequest_WithCookie_WithFoundPhoneNumberWithPersist_ReturnsFoundRecordAndSetCookie()
        {
            // Arrange
            // generate test data
            var dataModel = new PhoneManagerModel() { DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.PhoneManagerCampaignDetail =
                   new List<PhoneManagerCampaignDetail>()
                   {
                       new PhoneManagerCampaignDetail() { Id = "1201", TelephoneNumber = "0800 123 4567", CampaignCode = "testcode" },
                       new PhoneManagerCampaignDetail() { Id = "1202", TelephoneNumber = "0800 000 1111", CampaignCode = "testcode2" }
                   };
            var testPhoneManagerData = dataModel.ToXmlString();

            var AppParamHolder = new MockProviders.PhoneManagerAppParamHolder
                (
                    new CookieHolder(), // cookie
                    testPhoneManagerData, // repository data
                    new NameValueCollection() { {"fsource", "testcode2" }, { "dummykey", "dummyvalue" } }, // querystring
                    "", // referrer
                    new OutputModel(), // session
                    "" // umbraco current page id
                );

            // generate the required result
            PhoneManager app = new PhoneManager(AppParamHolder.CookieProvider, AppParamHolder.RepositoryProvider, AppParamHolder.QueryStringProvider, AppParamHolder.ReferrerProvider, AppParamHolder.SessionProvider, AppParamHolder.UmbracoProvider);

            //Act
            OutputModel actualResult = app.ProcessRequest();

            //Assert
            Assert.AreEqual("1202", actualResult.Id);
            Assert.AreEqual("0800 000 1111", actualResult.TelephoneNumber);
        }
    }
}
