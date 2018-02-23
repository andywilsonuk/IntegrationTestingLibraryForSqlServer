using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DistinctCollectionTests
    {
        private readonly IEqualityComparer<string> comparer = StringComparer.CurrentCultureIgnoreCase;
        private const string DefaultName = "p1";

        [Fact]
        public void Constructor_NullComparer_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new DistinctCollection<string>(null));
        }

        [Fact]
        public void Add_DuplicateInsert_Throws()
        {
            var names = new DistinctCollection<string>(comparer)
            {
                DefaultName
            };

            Assert.Throws<ArgumentException>(() => names.Add(DefaultName));
        }

        [Fact]
        public void Add_UniqueInsert_Added()
        {
            var names = new DistinctCollection<string>(comparer)
            {
                DefaultName,

                "p2"
            };

            Assert.Equal(2, names.Count);
        }

        [Fact]
        public void Set_DuplicateInsert_Throws()
        {
            var names = new DistinctCollection<string>(comparer)
            {
                DefaultName,
                "p2"
            };

            Assert.Throws<ArgumentException>(() => names[1] = DefaultName);
        }

        [Fact]
        public void AddRange_Single_Added()
        {
            var nameRange = new[] { DefaultName };

            var names = new DistinctCollection<string>(comparer);
            names.AddRange(nameRange);

            Assert.Single(names);
        }

        [Fact]
        public void AddRange_Duplicate_Throws()
        {
            var nameRange = new[] { DefaultName, DefaultName };
            
            var names = new DistinctCollection<string>(comparer);
            Assert.Throws<ArgumentException>(() => names.AddRange(nameRange));
        }
    }
}
