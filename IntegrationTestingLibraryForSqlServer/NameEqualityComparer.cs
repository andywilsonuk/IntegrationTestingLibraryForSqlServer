using System;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class NameEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, string> getName;

        public NameEqualityComparer(Func<T, string> nameResolver)
        {
            getName = nameResolver;
        }

        public bool Equals(T x, T y)
        {
            return string.Equals(getName(x), getName(y), StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(T item)
        {
            string name = getName(item);
            if (name == null) throw new ArgumentNullException(nameof(item));
            return name.GetHashCode();
        }
    }
}
