using System.Web.Routing;
using Umbraco.Core;

namespace Wr.UmbracoPhoneManager.Personalisation
{
    public class UmbracoApplicationEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapConfig.ConfigureAutoMapper();
        }
    }
}