using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class UmbracoProvider : IUmbracoProvider
    {
        public string GetCurrentPageId()
        {
            var publishContent = (IContent)Umbraco.Web.Composing.Current.UmbracoHelper.AssignedContentItem;
            int pageId = publishContent?.Id ?? 0;
            
            if (publishContent != null)
            {
                try
                {
                    var udi = publishContent.GetUdi().ToString();
                    return udi;
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