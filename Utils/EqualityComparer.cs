using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class EqualityComparer
    {
        // This comparer comapres only by defined func! (GetHashCode always returns 0)
        public static EqualityComparer<U> Get<U>(Func<U, U, bool> func)
        {
            return new EqualityComparer<U>(func);
        }
    }

    public class EqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> comparisonFunction;
        public EqualityComparer(Func<T, T, bool> func)
        {
            comparisonFunction = func;
        }

        public bool Equals(T x, T y)
        {
            return comparisonFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
