using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Personalisation
{
    public class PhoneManagerPersonalisationGroupAjaxController : Controller
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

            var result = Mapper.Map<List<PhoneManagerCampaignDetail>, List<PhoneManagerCriteriaSetting>>(recs);

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}