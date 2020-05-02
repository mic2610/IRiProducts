using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IRiProducts.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable value)
        {
            return value == null || !value.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Find the item within the sequence with the maximum value returned from the selector function
        /// </summary>
        public static TSource MaxBy<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
            where TValue : IComparable<TValue>
        {
            var isFirst = true;
            return source
                .Select(i => (Item: i, Value: selector(i)))
                .Aggregate(
                    default((TSource Item, TValue Value)),
                    (max, next) => (isFirst && !(isFirst = false)) || next.Value.CompareTo(max.Value) > 0 ? next : max).Item;
        }
    }
}