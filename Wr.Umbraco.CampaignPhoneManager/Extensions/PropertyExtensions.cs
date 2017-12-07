using System;
using System.Linq.Expressions;

namespace Wr.Umbraco.CampaignPhoneManager
{
    public static class PropertyExtensions
    {
        public static string Name<T, TProp>(this T o, Expression<Func<T, TProp>> propertySelector)
        {
            MemberExpression body = (MemberExpression)propertySelector.Body;
            return body.Member.Name;
        }
    }
}