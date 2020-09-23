using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class ExtensionMethods
    {

        public static T LazyLoadedFirstOrDefault<T>(this IEnumerable<T> items, Predicate<T> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default;
        }
    }
}
