using System;
using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Extensions;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class AvailableCriteria
    {
        private readonly List<ICriteriaDataProvider> CriteriaList = new List<ICriteriaDataProvider>();

        public AvailableCriteria(CriteriaParameterHolder criteriaParameters, IDataProvider iDataProvider)
        {
            CompileCriteriaList(criteriaParameters, iDataProvider);
        }

        /// <summary>
        /// Gets the available criteria
        /// </summary>
        /// <returns>Enumerable of available criteria</returns>
        public IEnumerable<ICriteriaDataProvider> GetCriteriaList()
        {
            return CriteriaList;
        }

        /// <summary>
        /// Discover the loaded assemblies of type 'ICampaignPhoneManagerCriteria'
        /// </summary>
        private void CompileCriteriaList(CriteriaParameterHolder criteriaParameters, IDataProvider iDataProvider)
        {

            var type = iDataProvider.InterfaceSelector();// typeof(ICampaignPhoneManagerCriteria);
            var criteriaClasses = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.GlobalAssemblyCache)
                .SelectMany(s => s.GetLoadableTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                .Select(x => Activator.CreateInstance(x, criteriaParameters) as ICriteriaDataProvider);

            foreach (var thisclass in criteriaClasses)
            {
                CriteriaList.Add(thisclass);
            }
        }
    }
}