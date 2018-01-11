using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using static Wr.UmbracoCampaignPhoneManager.Helpers.ENums;

namespace Wr.UmbracoCampaignPhoneManager
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Remove any unsafe characters based on the ProviderType. To prevent any form of malicious injection
        /// </summary>
        /// <param name="str"></param>
        /// <param name="providerType"></param>
        /// <returns>A safe string</returns>
        public static string ToSafeString(this String str, ProviderType providerType)
        {
            if (!string.IsNullOrEmpty(str))
            {
                switch (providerType)
                {
                    case ProviderType.QueryString:
                        return Regex.Replace(str, "[^A-Za-z0-9_-]", ""); // only allow alpha-numeric and _ - 

                    case ProviderType.Referrer:
                        return Regex.Replace(str, "[^A-Za-z0-9._-]", ""); // only allow alpha-numeric and . _ - 

                    default:
                        break;
                }
            }

            return str;
        }

        public static NameValueCollection ToSafeCollection(this NameValueCollection items, ProviderType providerType)
        {
            NameValueCollection safeCollection = new NameValueCollection();
            if (items != null)
            {
                foreach (string key in items)
                {
                    var cleanKey = key.ToSafeString(providerType);
                    var cleanValue = items[cleanKey].ToSafeString(providerType);
                    if (!string.IsNullOrEmpty(cleanKey) && !string.IsNullOrEmpty(cleanValue)) // only use qs where they have a value
                    {
                        safeCollection.Add(cleanKey, cleanValue);
                    }
                }
            }
            return safeCollection;
        }

        /// <summary>
        /// Convert Serializable object to XmlDocument
        /// </summary>
        /// <param name="oObject"></param>
        /// <returns></returns>
        public static XmlDocument ToXml(this object oObject)
        {
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty); // remomve default namespaces
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject, xns);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc;
            }
        }

        /// <summary>
        /// Convert Serializable object to XML string
        /// </summary>
        /// <param name="oObject"></param>
        /// <param name="incEncoding"></param>
        /// <returns></returns>
        public static string ToXmlString(this object oObject, bool incEncoding = false)
        {
            if (oObject.GetType().IsSerializable)
            {
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty); // remove default namespaces
                XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());

                if (incEncoding)
                {
                    string utf16;
                    using (StringWriter writer = new XmlHelper.SerializeXml.Utf16StringWriter())
                    {
                        xmlSerializer.Serialize(writer, oObject, xns);
                        utf16 = writer.ToString();
                    }
                    return utf16;
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    using (MemoryStream xmlStream = new MemoryStream())
                    {
                        xmlSerializer.Serialize(xmlStream, oObject, xns);
                        xmlStream.Position = 0;
                        xmlDoc.Load(xmlStream);
                        return xmlDoc.InnerXml;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Object must be serializable to use this extension method.");
            }
        }
    }
}