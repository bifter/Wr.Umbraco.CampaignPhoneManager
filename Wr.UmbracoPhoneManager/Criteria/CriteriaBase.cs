using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public abstract class CriteriaBase
    {
        protected bool MatchesValue(string valueFromContext, string valueFromDefinition)
        {
            if (valueFromContext == null)
            {
                return false;
            }

            return string.Equals(valueFromContext, valueFromDefinition,
                StringComparison.InvariantCultureIgnoreCase);
        }

        protected bool ContainsValue(string valueFromContext, string valueFromDefinition)
        {
            if (valueFromContext == null)
            {
                return false;
            }

            return CultureInfo.InvariantCulture.CompareInfo
                .IndexOf(valueFromContext, valueFromDefinition, CompareOptions.IgnoreCase) >= 0;
        }

        protected bool MatchesRegex(string valueFromContext, string valueFromDefinition)
        {
            if (valueFromContext == null)
            {
                return false;
            }

            return Regex.IsMatch(valueFromContext, valueFromDefinition);
        }

        protected bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

    }
}