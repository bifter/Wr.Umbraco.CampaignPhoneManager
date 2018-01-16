using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using Wr.UmbracoCampaignPhoneManager.Models;
using Wr.UmbracoCampaignPhoneManager.Providers.Storage;

namespace Wr.UmbracoCampaignPhoneManager.Plugins.UmbracoPersonalisationGroups
{
    public class CampaignPhoneManagerPersonalisationGroupAjaxController : Controller
    {
        // GET: Criteria
        public JsonResult Index()
        {
            return null;
        }

        public JsonResult ListCampaignDetails()
        {
            var _repository = new XPathRepository();
            var recs = _repository.ListAllCampaignDetailRecords();

            var result = Mapper.Map<List<CampaignDetail>, List<CampaignPhoneManagerCriteriaSetting>>(recs);

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}