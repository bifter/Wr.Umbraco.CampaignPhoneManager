using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public interface ICriteria
    {
        /// <summary>
        /// Returns all matching phonenumbers from the data
        /// </summary>
        /// <returns></returns>
        List<PhoneNumber> MatchingRecords();
    }
}