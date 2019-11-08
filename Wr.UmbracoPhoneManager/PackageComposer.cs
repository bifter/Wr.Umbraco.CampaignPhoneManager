using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Web.Composing;
using Umbraco.Core.Services.Implement;
using Umbraco.Web;
using Umbraco.Core.Composing;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager
{
    public class PackageComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<PhoneManagerApplicationInitComponent>();
            composition.Register<IPhoneManagerService, PhoneManagerService>(Lifetime.Request);
        }

        public class PhoneManagerApplicationInitComponent : IComponent
        {
            // initialize: runs once when Umbraco starts
            public void Initialize()
            {
                UmbracoApplicationBase.ApplicationInit += PhoneManagerApplicationInit;
            }

            private void PhoneManagerApplicationInit(object sender, EventArgs e)
            {
                var application = (HttpApplication)sender;
                application.PreRequestHandlerExecute += doPhoneManagerProcessing;
            }

            // terminate: runs once when Umbraco stops
            public void Terminate()
            { }

            private void doPhoneManagerProcessing(object sender, EventArgs e)
            {
                
                var currentPageId = Umbraco.Web.Composing.Current.UmbracoHelper.AssignedContentItem?.Id;
                if (!currentPageId.HasValue)
                {
                    return;
                }

                var phoneManagerResult = new PhoneManager().ProcessRequest();
            }
        }
    }
}