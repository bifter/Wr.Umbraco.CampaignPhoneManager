using System;
using System.Web;
using Umbraco.Core;
using Umbraco.Web;

namespace Wr.UmbracoCampaignPhoneManager.Events
{
    public class UmbracoApplicationEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //if (!Setup.DoAllDocTypesExist())
            //    Setup.CreateDocTypes();

            UmbracoApplicationBase.ApplicationInit += CampaignPhoneManagerApplicationInit;
        }

        private void CampaignPhoneManagerApplicationInit(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            application.PreRequestHandlerExecute += doCampaignPhoneManagerProcessing;
        }

        private void doCampaignPhoneManagerProcessing(object sender, EventArgs e)
        {
            //var httpContext = HttpContext.Current;
            var umbracoContext = UmbracoContext.Current;

            if (umbracoContext?.PageId == null)
                return;

            var phoneManagerResult = new CampaignPhoneManagerApp().ProcessRequest();

            // save the selected phone manager data in the IPublishedContent for use on the reqested page
            //umbracoContext.PublishedContentRequest.PublishedContent.CampaignPhoneManager(phoneManagerResult);
            
            
        }


    }
}