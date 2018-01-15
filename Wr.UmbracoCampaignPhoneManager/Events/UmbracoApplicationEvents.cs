using System;
using System.Web;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web;
using Wr.UmbracoCampaignPhoneManager.App_Config;

namespace Wr.UmbracoCampaignPhoneManager.Events
{
    public class UmbracoApplicationEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapConfig.ConfigureAutoMapper();

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
        }
    }
}