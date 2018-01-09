using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Tests.Models
{
    [TestClass]
    public class CampaignPhoneManagerModelTest
    {
        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_DefaultPersistDurationInDays_NotSet_AlwaysReturnsAPositiveInt()
        {
            // Arrange
            var model = new CampaignPhoneManagerModel();         

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_DefaultPersistDurationInDays_ZeroNumberSet_AlwaysReturnsAPositiveInt()
        {
            // Arrange
            var model = new CampaignPhoneManagerModel();
            model.DefaultPersistDurationInDays = 0;

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_DefaultPersistDurationInDays_MinusNumberSet_AlwaysReturnsAPositiveInt()
        {
            // Arrange
            var model = new CampaignPhoneManagerModel();
            model.DefaultPersistDurationInDays = -1;

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_DefaultPersistDurationInDays_ValidNumberSet_ReturnsValidNumber()
        {
            // Arrange
            var model = new CampaignPhoneManagerModel();
            model.DefaultPersistDurationInDays = 10;

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_IsValid_InputNotValid_ReturnsFalse()
        {
            // Arrange
            var model = new CampaignDetail();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_IsValid_InputValid_ReturnsTrue()
        {
            // Arrange
            var model = new CampaignDetail() { TelephoneNumber = "0800" };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_IsValidToSaveAsCookie_NoInput_ReturnsFalse()
        {
            // Arrange
            var model = new CampaignDetail();

            // Act
            var result = model.IsValidToSaveAsCookie();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_IsValidToSaveAsCookie_ValidInput_ReturnsTrue()
        {
            // Arrange
            var model = new CampaignDetail() { TelephoneNumber = "0800", DoNotPersistAcrossVisits = false };

            // Act
            var result = model.IsValidToSaveAsCookie();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_NumberOfPropertiesSameAsNumberOfXmlElementNames_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<CampaignDetail>();

            // Assert
            Assert.IsTrue(result.ElementNameCount == result.PropertyCount);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_AllXmlElementNamesAreUnique_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<CampaignDetail>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.UniqueXmlNames.Count);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignDetailTest_XmlElementNameMatchPropertyName_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<CampaignDetail>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.PropertyNameMatchingElementNameCount);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_NumberOfPropertiesSameAsNumberOfXmlElementNames_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<CampaignPhoneManagerModel>();

            // Assert
            Assert.IsTrue(result.ElementNameCount == result.PropertyCount);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_AllXmlElementNamesAreUnique_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<CampaignPhoneManagerModel>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.UniqueXmlNames.Count);
        }

        [TestMethod]
        public void Model_CampaignPhoneManagerModel_CampaignPhoneManagerModelTest_XmlElementNameMatchPropertyName_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<CampaignPhoneManagerModel>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.PropertyNameMatchingElementNameCount);
        }


        #region >>>> Helpers

        /// <summary>
        /// Use reflection to get the custom attributes of all the properties in the passed in class <T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private PropertyInfoHolder GetCustomPropertyInfo<T>()
        {
            Type type = typeof(T);
            var propertyCount = 0;
            var elementNameCount = 0;
            var propertyNameMatchingElementNameCount = 0;

            List<string> uniqueElementNames = new List<string>();

            // Act
            foreach (var propertyInfo in type.GetProperties())
            {
                // each property
                propertyCount++;

                foreach (var attribute in propertyInfo.GetCustomAttributes(true))
                {
                    // each custom attribute in the property
                    if (attribute is XmlElementAttribute || attribute is XmlAttributeAttribute)
                    {
                        if (attribute is XmlElementAttribute)
                        {
                            var elementName = ((XmlElementAttribute)attribute).ElementName;
                            if (!string.IsNullOrEmpty(elementName))
                            {
                                elementNameCount++;

                                if (elementName.ToLower() == propertyInfo.Name.ToLower())
                                    propertyNameMatchingElementNameCount++;


                                if (!uniqueElementNames.Contains(elementName))
                                    uniqueElementNames.Add(elementName);

                                break;
                            }
                        }
                        else // if XmlAttributeAttribute
                        {
                            var elementName = ((XmlAttributeAttribute)attribute).AttributeName;
                            if (!string.IsNullOrEmpty(elementName))
                            {
                                elementNameCount++;

                                if (elementName.ToLower() == propertyInfo.Name.ToLower())
                                    propertyNameMatchingElementNameCount++;

                                if (!uniqueElementNames.Contains(elementName))
                                    uniqueElementNames.Add(elementName);

                                break;
                            }
                        }
                    }
                }
            }

            var result = new PropertyInfoHolder()
            {
                PropertyCount = propertyCount,
                ElementNameCount = elementNameCount,
                PropertyNameMatchingElementNameCount = propertyNameMatchingElementNameCount,
                UniqueXmlNames = uniqueElementNames
            };

            return result;
        }

        private class PropertyInfoHolder
        {
            public int PropertyCount { get; set; }
            public int ElementNameCount { get; set; }
            public int PropertyNameMatchingElementNameCount { get; set; }
            public List<string> UniqueXmlNames { get; set; }

            public PropertyInfoHolder()
            {
                UniqueXmlNames = new List<string>();
            }
        }

        #endregion


    }
}
