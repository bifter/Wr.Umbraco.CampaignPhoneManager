using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Umbraco.Core;

namespace Wr.UmbracoPhoneManager.Extensions
{
    /// <summary>
    /// Extension methods to Assembly
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Safely loads types from an assembly avoiding errors if ones can't be loaded
        /// </summary>
        /// <param name="assembly">Instance of an Assembly</param>
        /// <returns>Enumerable of loadable types</returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            Mandate.ParameterNotNull(assembly, "assembly");

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}