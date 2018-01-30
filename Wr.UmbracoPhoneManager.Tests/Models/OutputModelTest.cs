using NUnit.Framework;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Tests.Models
{
    [TestFixture]
    public class OutputModelTest
    {
        [Test]
        public void Model_OutputModel_IsValidTest_NotSet_ReturnsFalse()
        {
            // Arrange
            var model = new OutputModel();

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase("1", "")]
        [TestCase("1", null)]
        [TestCase("", "0800 232 4353")]
        [TestCase(null, "0800 232 4353")]
        public void Model_OutputModel_IsValidTest_NotValid_ReturnsFalse(string id, string phonenumber)
        {
            // Arrange
            var model = new OutputModel() { Id = id, TelephoneNumber = phonenumber };

            // Act
            var result = model.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
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
