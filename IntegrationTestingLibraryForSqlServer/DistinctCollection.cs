using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class DistinctCollection<T> : Collection<T>
    {
        private IEqualityComparer<T> equalityComparer;

        public DistinctCollection(IEqualityComparer<T> comparer) : base()
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            equalityComparer = comparer;
        }

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                EnsureUnique(item);
                Add(item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            EnsureUnique(item);
            base.InsertItem(index, item);
        }

        private void EnsureUnique(T item)
        {
            if (Items.Any(x => equalityComparer.Equals(x, item)))
                throw new ArgumentException($"A parameter that compares the same already exists, the additional item is: {item}", nameof(item));
        }

        protected override void SetItem(int index, T item)
        {
            EnsureUnique(item);
            base.SetItem(index, item);
        }
    }
}
