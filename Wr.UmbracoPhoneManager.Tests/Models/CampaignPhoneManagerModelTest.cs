﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Tests.Models
{
    [TestFixture]
    public class CampaignPhoneManagerModelTest
    {
        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_DefaultPersistDurationInDays_NotSet_AlwaysReturnsAPositiveInt()
        {
            // Arrange
            var model = new PhoneManagerModel();         

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_DefaultPersistDurationInDays_ZeroNumberSet_AlwaysReturnsAPositiveInt()
        {
            // Arrange
            var model = new PhoneManagerModel();
            model.DefaultPersistDurationInDays = 0;

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_DefaultPersistDurationInDays_MinusNumberSet_AlwaysReturnsAPositiveInt()
        {
            // Arrange
            var model = new PhoneManagerModel();
            model.DefaultPersistDurationInDays = -1;

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_DefaultPersistDurationInDays_ValidNumberSet_ReturnsValidNumber()
        {
            // Arrange
            var model = new PhoneManagerModel();
            model.DefaultPersistDurationInDays = 10;

            // Act
            var result = model.DefaultPersistDurationInDays;

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_IsValid_InputNotValid_ReturnsFalse()
        {
            // Arrange
            var model = new PhoneManagerCampaignDetail();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_IsValid_InputValid_ReturnsTrue()
        {
            // Arrange
            var model = new PhoneManagerCampaignDetail() { TelephoneNumber = "0800" };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_IsValidToSaveAsCookie_NoInput_ReturnsFalse()
        {
            // Arrange
            var model = new PhoneManagerCampaignDetail();

            // Act
            var result = model.IsValidToSaveAsCookie();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_IsValidToSaveAsCookie_ValidInput_ReturnsTrue()
        {
            // Arrange
            var model = new PhoneManagerCampaignDetail() { TelephoneNumber = "0800", DoNotPersistAcrossVisits = false };

            // Act
            var result = model.IsValidToSaveAsCookie();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_NumberOfPropertiesSameAsNumberOfXmlElementNames_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<PhoneManagerCampaignDetail>();

            // Assert
            Assert.IsTrue(result.ElementNameCount == result.PropertyCount);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_AllXmlElementNamesAreUnique_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<PhoneManagerCampaignDetail>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.UniqueXmlNames.Count);
        }

        [Test]
        public void Model_PhoneManagerModel_CampaignDetailTest_XmlElementNameMatchPropertyName_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<PhoneManagerCampaignDetail>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.PropertyNameMatchingElementNameCount);
        }

        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_NumberOfPropertiesSameAsNumberOfXmlElementNames_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<PhoneManagerModel>();

            // Assert
            Assert.IsTrue(result.ElementNameCount == result.PropertyCount);
        }

        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_AllXmlElementNamesAreUnique_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<PhoneManagerModel>();

            // Assert
            Assert.IsTrue(result.PropertyCount == result.UniqueXmlNames.Count);
        }

        [Test]
        public void Model_PhoneManagerModel_PhoneManagerModelTest_XmlElementNameMatchPropertyName_ReturnsTrue()
        {
            // Arrange / Act
            var result = GetCustomPropertyInfo<PhoneManagerModel>();

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
