using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{
    public interface IXPathDataSource
    {
        /// <summary>
        /// Get default settings from the source data store
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        DefaultSettings LoadDefaultSettings(string xpath);

        /// <summary>
        /// Get data from the source data store
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        List<PhoneNumber> GetDataByXPath(string xpath);
    }
}