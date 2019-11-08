using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Configuration;
using Wr.UmbracoPhoneManager.App_Config;
using Wr.UmbracoPhoneManager.Extensions;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public class AvailableCriteria
    {
        private static IEnumerable<IPhoneManagerCriteria> _criteriaList;

        static AvailableCriteria() { }

        /// <summary>
        /// Gets the available criteria
        /// </summary>
        /// <returns>Enumerable of available criteria</returns>
        public static IEnumerable<IPhoneManagerCriteria> GetCriteriaList()
        {
            if (_criteriaList == null)
                _criteriaList = CompileCriteriaList();

            return _criteriaList;
        }

        /// <summary>
        /// Discover the loaded assemblies of type 'IPhoneManagerCriteria', or use the hardcoded list for speed
        /// </summary>
        private static IEnumerable<IPhoneManagerCriteria> CompileCriteriaList()
        {
            AppSettingsConfig appSettingsConfig = new AppSettingsConfig();

            if (appSettingsConfig.DiscoverNewCriteria)
            {
                var type = typeof(IPhoneManagerCriteria);
                var criteriaClasses = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.GlobalAssemblyCache)
                    .SelectMany(s => s.GetLoadableTypes())
                    .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                    .Select(x => Activator.CreateInstance(x) as IPhoneManagerCriteria);

                return criteriaClasses?.ToList();
            }
            else
            {
                var hardCodedList = new List<IPhoneManagerCriteria>()
                {
                    new QueryStringCriteria(),
                    new ReferrerCriteria(),
                    new EntryPageCriteria()
                };
                return hardCodedList;
            }
        }
    }
}