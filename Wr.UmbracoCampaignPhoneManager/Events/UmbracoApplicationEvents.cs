using Examine;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;
using System.Xml;
using umbraco.NodeFactory;
using Umbraco.Core;
using Umbraco.Web;
using UmbracoExamine;
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

            ExamineManager.Instance.IndexProviderCollection["InternalIndexer"].GatheringNodeData += GeneralExamineEvents_GatheringNodeData;
        }

        private void CampaignPhoneManagerApplicationInit(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            application.PreRequestHandlerExecute += doCampaignPhoneManagerProcessing;
        }

        private void doCampaignPhoneManagerProcessing(object sender, EventArgs e)
        {
            var umbracoContext = UmbracoContext.Current;

            if (umbracoContext?.PageId == null)
                return;

            var phoneManagerResult = new CampaignPhoneManagerApp().ProcessRequest();
        }

        // Testing to try and convert EntyPage content picker value to the url path or name rather than the not very meaningful udi of the content 
        void GeneralExamineEvents_GatheringNodeData(object sender, IndexingNodeDataEventArgs e)
        {
            if (e.IndexType != IndexTypes.Content)
                return;

            if (e.Fields.ContainsKey("nodeTypeAlias"))
            {
                if (e.Fields["nodeTypeAlias"] == AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail)
                {
                    var result = "/about-us/"; // testing
                    Debug.WriteLine("GatheringNodeData: CampaignDetail found!");
                    
                    var node = new Node(e.NodeId);

                    foreach (var prop in node.PropertiesAsList)
                    {
                        Debug.WriteLine("GatheringNodeData: Children: " + prop.Alias + " - " + prop.Value);
                        if (prop.Alias == AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage)
                        {
                            //e.Fields.Add("entryPageUrl", e.Node.Parent.GetProperty("Name").Value.ToString());
                            Debug.WriteLine("GatheringNodeData: entryPage found!");
                            e.Fields.Add("entryPageUrl", result);

                            var xnode = new Node();
                            xnode.Name = "entryPageUrl";
                            var newprop = new Property("",);
                            newprop.Alias = "entryPageUrl";

                            node.Properties.Add(new Property("",) { }"entryPageUrl", );
                            break;
                        }
                    }

                    

                }
            }
        }
    }
}