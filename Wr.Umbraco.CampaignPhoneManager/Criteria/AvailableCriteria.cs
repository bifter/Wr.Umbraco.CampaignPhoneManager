using System;
using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Extensions;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public static class AvailableCriteria
    {
        private static IEnumerable<ICriteria> _criteriaList;
        private static IRepository _repository;

        static AvailableCriteria() { }

        /// <summary>
        /// Gets the available criteria
        /// </summary>
        /// <returns>Enumerable of available criteria</returns>
        public static IEnumerable<ICriteria> GetCriteriaList(CriteriaParameterHolder criteriaParameters, IRepository repository)
        {
            if (_repository == null)
                _repository = repository;

            if (_criteriaList == null)
                _criteriaList = CompileCriteriaList(criteriaParameters);

            return _criteriaList;
        }

        /// <summary>
        /// Discover the loaded assemblies of type 'ICampaignPhoneManagerCriteria'
        /// </summary>
        private static IEnumerable<ICriteria> CompileCriteriaList(CriteriaParameterHolder criteriaParameters)
        {

            var type = typeof(ICampaignPhoneManagerCriteria);
            var criteriaClasses = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.GlobalAssemblyCache)
                .SelectMany(s => s.GetLoadableTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                .Select(x => Activator.CreateInstance(x, criteriaParameters, _repository) as ICriteria);

            return criteriaClasses?.ToList();
        }
    }
}