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
            application.PreRequestHandlerExecute += doPhoneManagerProcessing;
        }

        private void doPhoneManagerProcessing(object sender, EventArgs e)
        {
            var umbracoContext = UmbracoContext.Current;

            if (umbracoContext?.PageId == null)
                return;

            var phoneManagerResult = new PhoneManager().ProcessRequest();
        }
    }
}