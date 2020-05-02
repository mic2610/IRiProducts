using System.Collections;

namespace IRiProducts.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable value)
        {
            return value == null || !value.GetEnumerator().MoveNext();
        }
    }
}