using System.Web.Mvc;
using System.Web.Routing;

namespace Wr.UmbracoCampaignPhoneManager.App_Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Campaign UmbracoPersonalisationGroups",
                url: "App_Plugins/UmbracoPersonalisationGroups/CampaignPhoneManager/{action}",
                defaults: new { controller = "CampaignPhoneManagerPersonalisationGroupAjax", action = "Index" });

        }
    }
}