using System;
using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Extensions;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public static class AvailableCriteria
    {
        private static readonly List<ICampaignPhoneManagerCriteria> CriteriaList = new List<ICampaignPhoneManagerCriteria>();

        static AvailableCriteria()
        {
            CompileCriteriaList();
        }

        /// <summary>
        /// Gets the available criteria
        /// </summary>
        /// <returns>Enumerable of available criteria</returns>
        public static IEnumerable<ICampaignPhoneManagerCriteria> GetCriteriaList()
        {
            return CriteriaList;
        }

        /// <summary>
        /// Discover the loaded assemblies of type 'ICampaignPhoneManagerCriteria'
        /// </summary>
        private static void CompileCriteriaList()
        {

            var type = typeof(ICampaignPhoneManagerCriteria);
            var criteriaClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetLoadableTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                .Select(x => Activator.CreateInstance(x) as ICampaignPhoneManagerCriteria);

            foreach (var thisclass in criteriaClasses)
            {
                CriteriaList.Add(thisclass);
            }
        }
    }
}