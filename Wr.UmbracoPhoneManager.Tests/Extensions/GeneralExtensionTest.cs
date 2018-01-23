using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Linq;
using static Wr.UmbracoPhoneManager.Helpers.ENums;

namespace Wr.UmbracoPhoneManager.Tests.Extensions
{
    [TestClass]
    public class GeneralExtensionTest
    {
        [DataTestMethod]
        [DataRow("thisissafe", ProviderType.Referrer)]
        [DataRow("thisissafe.com", ProviderType.Referrer)]
        [DataRow("thisissafe", ProviderType.QueryString)]
        [DataRow("thisissafecom", ProviderType.QueryString)]
        public void GeneralExtension_ToSafeStringTest_SafeString_ReturnsSameString(string input, ProviderType providerType)
        {
            // Arrange & Act
            var cleanString = input.ToSafeString(providerType);

            // Assert
            Assert.AreEqual(cleanString, input);
        }

        [DataTestMethod]
        [DataRow("th/isi'='0'nots.af!e", "thisi0nots.afe", ProviderType.Referrer)]
        [DataRow("thisi'='0's nots.afe", "thisi0snots.afe", ProviderType.Referrer)]
        [DataRow("th/isis'='0'no.ts!afe", "thisis0notsafe", ProviderType.QueryString)]
        [DataRow("thisis'='0'no.tsa fe", "thisis0notsafe", ProviderType.QueryString)]
        public void GeneralExtension_ToSafeStringTest_UnsafeString_ReturnsSafeString(string input, string correctResult, ProviderType providerType)
        {
            // Arrange & Act
            var cleanString = input.ToSafeString(providerType);

            // Assert
            Assert.AreEqual(cleanString, correctResult);
        }

        [TestMethod]
        public void GeneralExtension_ToSafeCollectionTest_Referrer_SafeCollection_ReturnsSameCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection() { { "safekey", "safevalue.com" }, { "safekey2", "safevalue2.co.uk" } };

            // Act
            var cleanCollection = collection.ToSafeCollection(ProviderType.Referrer);

            // Assert
            CollectionAssert.AreEquivalent(
                collection.AllKeys.ToDictionary(k => k, k => cleanCollection[k]),
                cleanCollection.AllKeys.ToDictionary(k => k, k => collection[k]));
        }

        [TestMethod]
        public void GeneralExtension_ToSafeCollectionTest_Referrer_UnSafeCollection_ReturnsExpectedCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection() { { "safekey", "safe'value.com" }, { "safekey2", "safev= alue2.co.uk" } };
            NameValueCollection expectedCollection = new NameValueCollection() { { "safekey", "safevalue.com" }, { "safekey2", "safevalue2.co.uk" } };
            // Act
            var cleanCollection = collection.ToSafeCollection(ProviderType.Referrer);

            // Assert
            CollectionAssert.AreEquivalent(
                expectedCollection.AllKeys.ToDictionary(k => k, k => cleanCollection[k]),
                cleanCollection.AllKeys.ToDictionary(k => k, k => expectedCollection[k]));
        }

        [TestMethod]
        public void GeneralExtension_ToSafeCollectionTest_QueryString_SafeCollection_ReturnsSameCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection() { { "safekey", "safevalue" }, { "safekey2", "safevalue2" } };

            // Act
            var cleanCollection = collection.ToSafeCollection(ProviderType.QueryString);

            // Assert
            CollectionAssert.AreEquivalent(
                collection.AllKeys.ToDictionary(k => k, k => cleanCollection[k]),
                cleanCollection.AllKeys.ToDictionary(k => k, k => collection[k]));
        }

        [TestMethod]
        public void GeneralExtension_ToSafeCollectionTest_QueryString_UnSafeCollection_ReturnsExpectedCollection()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection() { { "safekey", "safeva' lue.com" }, { "safekey2", "safev= alue2.co.uk" } };
            NameValueCollection expectedCollection = new NameValueCollection() { { "safekey", "safevaluecom" }, { "safekey2", "safevalue2couk" } };
            // Act
            var cleanCollection = collection.ToSafeCollection(ProviderType.QueryString);

            // Assert
            CollectionAssert.AreEquivalent(
                expectedCollection.AllKeys.ToDictionary(k => k, k => cleanCollection[k]),
                cleanCollection.AllKeys.ToDictionary(k => k, k => expectedCollection[k]));
        }

    }
}
