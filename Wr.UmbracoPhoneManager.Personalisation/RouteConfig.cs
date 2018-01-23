using System.Web.Mvc;
using System.Web.Routing;

namespace Wr.UmbracoPhoneManager.Personalisation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Campaign UmbracoPersonalisationGroups",
                url: "App_Plugins/UmbracoPersonalisationGroups/PhoneManager/{action}",
                defaults: new { controller = "PhoneManagerPersonalisationGroupAjax", action = "Index" });

        }
    }
}