using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSCPortal.Framework;

namespace PSCPortal.Libs
{
    public static class IEnumerableExtentionMethods
    {
        public static IEnumerable<T> GetSegmentList<T>(this IEnumerable<T> collection, int startIndex, int maximumRows, string sortExpressions)
        {
            SortExpressionCollection sortExpressionList = null;
            if (sortExpressions != string.Empty)
                sortExpressionList = SortExpressionCollection.Parse(sortExpressions);
            if (sortExpressionList != null)
                return Sort(collection, sortExpressionList).Skip(startIndex).Take(maximumRows);

            return collection.Skip(startIndex).Take(maximumRows);
        }
        private static IOrderedEnumerable<T> Sort<T>(IEnumerable<T> collection, SortExpressionCollection sortExpressions)
        {
            IOrderedEnumerable<T> result = null;
            if (sortExpressions[0].Order == SortExpression.ORDER.ASC)
                result = collection.OrderBy(s => GetPropertySort(s, sortExpressions[0].PropertyName));
            else
                result = collection.OrderByDescending(s => GetPropertySort(s, sortExpressions[0].PropertyName));
            for (int i = 1; i < sortExpressions.Count; i++)
            {
                SortExpression sort = sortExpressions[i];
                if (sort.Order == SortExpression.ORDER.ASC)
                    result = result.ThenBy(s => GetPropertySort(s, sort.PropertyName));
                else
                    result = result.ThenByDescending(s => GetPropertySort(s, sort.PropertyName));
            }
            return result;
        }
        private static IComparable GetPropertySort(object obj, string propertyName)
        {
            IComparable result = (IComparable)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
            return result;
        }
    }
}