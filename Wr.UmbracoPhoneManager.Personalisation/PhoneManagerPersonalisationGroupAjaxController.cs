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

        /// <summary>
        /// Returns a list of all Phone Manager campaign details. If this is a multiple site umbraco system then this will return records from all sites. With this in mind it would be worth including a prefix in the phone manager detila to specify which site the phone number details is for.
        /// </summary>
        /// <returns></returns>
        public JsonResult ListCampaignDetails()
        {
            var _repository = new XPathRepository();
            var recs = _repository.ListAllCampaignDetailRecords();

            var result = Mapper.Map<List<PhoneManagerCampaignDetail>, List<PhoneManagerCriteriaSetting>>(recs);

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}