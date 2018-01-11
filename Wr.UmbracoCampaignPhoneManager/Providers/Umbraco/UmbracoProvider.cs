using System;
using Umbraco.Core;
using Umbraco.Web;

namespace Wr.UmbracoCampaignPhoneManager.Providers
{
    public class UmbracoProvider : IUmbracoProvider
    {
        public string GetCurrentPageId()
        {
            var umbracoContext = UmbracoContext.Current;
            int pageId = umbracoContext?.PageId ?? 0;

            string result = string.Empty;

            var node = ApplicationContext.Current.Services.ContentService.GetById(pageId); // get current page object
            if (node != null)
            {
                try
                {
                    result = node.GetUdi().ToString(); // this version for Umbraco 7.6.0+ - Use Udi
                    return result;
                }
                catch(Exception ex)
                {
                    // Pre Umbraco 7.6.0 ??
                }
            }
            if (pageId > 0)
            {
                return pageId.ToString(); // otherwise use PageId
            }

            return null;
        }
    }
}