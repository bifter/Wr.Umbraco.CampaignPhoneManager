using System;
using System.Web;
using Umbraco.Core;
using Umbraco.Web;

namespace Wr.UmbracoPhoneManager.Events
{
    public class UmbracoApplicationEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UmbracoApplicationBase.ApplicationInit += PhoneManagerApplicationInit;
        }

        private void PhoneManagerApplicationInit(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            application.PreRequestHandlerExecute += doCampaignPhoneManagerProcessing;
        }

        private void doCampaignPhoneManagerProcessing(object sender, EventArgs e)
        {
            //var httpContext = HttpContext.Current;
            var umbracoContext = UmbracoContext.Current;

            if (umbracoContext?.PageId == null)
            {
                return;
            }

            var phoneManagerResult = new CampaignPhoneManager().ProcessRequest();

            // save the selected phone manager data in the IPublishedContent for use on the reqested page
            umbracoContext.PublishedContentRequest.PublishedContent.CampaignPhoneManager(phoneManagerResult);
            
            
        }


    }
}