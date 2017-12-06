using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml.XPath;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager
{
    public static class XmlHelper
    {
        /// <summary>
        /// Deserialise XPathNavigator item to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="navigatorItem"></param>
        /// <returns></returns>
        public static List<CampaignDetail> XPathNavigatorToModel(IEnumerable<XPathNavigator> navigatorItems)
        {
            List<CampaignDetail> result = new List<CampaignDetail>();
            foreach (var item in navigatorItems)
            {
                var serializer = new XmlSerializer(typeof(CampaignDetail));
                var textResult = new StringReader(item.OuterXml);
                try
                {
                    var tmp = (CampaignDetail)serializer.Deserialize(textResult);
                    if (tmp != null)
                        result.Add(tmp);
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        /// <summary>
        /// Deserialise XPathNavigator items to Model list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="navigatorItem"></param>
        /// <returns></returns>
        public static T XPathNavigatorToModel<T>(XPathNavigator navigatorItem) where T : class, new()
        {
            T result = null; // = new T();
            if (navigatorItem != null)
            {
                var serializer = new XmlSerializer(typeof(T));
                var textResult = new StringReader(navigatorItem.OuterXml);
                try
                {
                    result = (T)serializer.Deserialize(textResult);
                }
                catch(Exception ex)
                {

                }
            }
            return result;
        }
    }
}