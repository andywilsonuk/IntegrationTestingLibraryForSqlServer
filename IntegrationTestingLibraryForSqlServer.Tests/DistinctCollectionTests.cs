using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DistinctCollectionTests
    {
        private readonly IEqualityComparer<string> comparer = StringComparer.CurrentCultureIgnoreCase;
        private const string DefaultName = "p1";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullComparer_Throws()
        {
            new DistinctCollection<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_DuplicateInsert_Throws()
        {
            var names = new DistinctCollection<string>(comparer);
            names.Add(DefaultName);

            names.Add(DefaultName);
        }

        [TestMethod]
        public void Add_UniqueInsert_Added()
        {
            var names = new DistinctCollection<string>(comparer);
            names.Add(DefaultName);

            names.Add("p2");

            Assert.AreEqual(2, names.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Set_DuplicateInsert_Throws()
        {
            var names = new DistinctCollection<string>(comparer);
            names.Add(DefaultName);
            names.Add("p2");

            names[1] = DefaultName;
        }

        [TestMethod]
        public void AddRange_Single_Added()
        {
            var nameRange = new[] { DefaultName };

            var names = new DistinctCollection<string>(comparer);
            names.AddRange(nameRange);

            Assert.AreEqual(1, names.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddRange_Duplicate_Throws()
        {
            var nameRange = new[] { DefaultName, DefaultName };
            
            var names = new DistinctCollection<string>(comparer);
            names.AddRange(nameRange);

            Assert.AreEqual(1, names.Count);
        }
    }
}
