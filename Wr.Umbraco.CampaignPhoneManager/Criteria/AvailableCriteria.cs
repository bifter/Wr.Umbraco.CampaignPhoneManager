using System;
using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Extensions;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class AvailableCriteria
    {
        private static IEnumerable<ICampaignPhoneManagerCriteria> _criteriaList;
        //private static IRepository _repository;

        static AvailableCriteria() { }

        /// <summary>
        /// Gets the available criteria
        /// </summary>
        /// <returns>Enumerable of available criteria</returns>
        /*public static IEnumerable<ICampaignPhoneManagerCriteria> GetCriteriaList(CriteriaParameterHolder criteriaParameters, IRepository repository)
        {
            if (_repository == null)
                _repository = repository;

            if (_criteriaList == null)
                _criteriaList = CompileCriteriaList();

            return _criteriaList;
        }*/

        public static IEnumerable<ICampaignPhoneManagerCriteria> GetCriteriaList()
        {
            if (_criteriaList == null)
                _criteriaList = CompileCriteriaList();

            return _criteriaList;
        }

        /// <summary>
        /// Discover the loaded assemblies of type 'ICampaignPhoneManagerCriteria'
        /// </summary>
        /*private static IEnumerable<ICampaignPhoneManagerCriteria> CompileCriteriaList(CriteriaParameterHolder criteriaParameters)
        {

            var type = typeof(ICampaignPhoneManagerCriteria);
            var criteriaClasses = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.GlobalAssemblyCache)
                .SelectMany(s => s.GetLoadableTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                .Select(x => Activator.CreateInstance(x, criteriaParameters, _repository) as ICampaignPhoneManagerCriteria);

            return criteriaClasses?.ToList();
        }*/

        private static IEnumerable<ICampaignPhoneManagerCriteria> CompileCriteriaList()
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