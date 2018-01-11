using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Tests.Models
{
    [TestClass]
    public class OutputModelTest
    {
        [TestMethod]
        public void Model_OutputModel_IsValidTest_NotSet_ReturnsFalse()
        {
            // Arrange
            var model = new OutputModel();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("", "")]
        [DataRow(null, null)]
        [DataRow("1", "")]
        [DataRow("1", null)]
        [DataRow("", "0800 232 4353")]
        [DataRow(null, "0800 232 4353")]
        public void Model_OutputModel_IsValidTest_NotValid_ReturnsFalse(string id, string phonenumber)
        {
            // Arrange
            var model = new OutputModel() { Id = id, TelephoneNumber = phonenumber };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Model_OutputModel_IsValidTest_InputValid_ReturnsTrue()
        {
            // Arrange
            var model = new OutputModel() { Id = "1", TelephoneNumber = "0800" };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
