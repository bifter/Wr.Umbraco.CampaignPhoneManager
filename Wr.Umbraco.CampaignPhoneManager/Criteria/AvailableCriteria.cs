using System;
using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Extensions;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class AvailableCriteria
    {
        private static IEnumerable<ICampaignPhoneManagerCriteria> _criteriaList;

        static AvailableCriteria() { }

        /// <summary>
        /// Gets the available criteria
        /// </summary>
        /// <returns>Enumerable of available criteria</returns>
        public static IEnumerable<ICampaignPhoneManagerCriteria> GetCriteriaList()
        {
            if (_criteriaList == null)
                _criteriaList = CompileCriteriaList(true);

            return _criteriaList;
        }

        /// <summary>
        /// Discover the loaded assemblies of type 'ICampaignPhoneManagerCriteria', or use the hardcoded list for speed
        /// </summary>
        private static IEnumerable<ICampaignPhoneManagerCriteria> CompileCriteriaList(bool useHardcoded = false)
        {

            if (useHardcoded)
            {
                var hardCodedList = new List<ICampaignPhoneManagerCriteria>()
                {
                    new QueryStringCriteria(),
                    new ReferrerCriteria(),
                    new EntryPageCriteria()
                };
                return hardCodedList;
            }
            else
            {
                var type = typeof(ICampaignPhoneManagerCriteria);
                var criteriaClasses = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.GlobalAssemblyCache)
                    .SelectMany(s => s.GetLoadableTypes())
                    .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                    .Select(x => Activator.CreateInstance(x) as ICampaignPhoneManagerCriteria);

                return criteriaClasses?.ToList();
            }
        }
    }
}