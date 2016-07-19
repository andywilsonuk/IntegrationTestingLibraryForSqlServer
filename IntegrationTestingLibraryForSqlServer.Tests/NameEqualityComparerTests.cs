using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class NameEqualityComparerTests
    {
        private NameEqualityComparer<string> comparer = new NameEqualityComparer<string>(input => input);
        private const string name = "@p1";

        [TestMethod]
        public void Equals_NullX_False()
        {
            bool actual = comparer.Equals(null, name);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_NullY_False()
        {
            bool actual = comparer.Equals(name, null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_NullXY_True()
        {
            bool actual = comparer.Equals(null, null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_SameXY_True()
        {
            bool actual = comparer.Equals(name, name);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_EquivalentY_True()
        {
            string nameY = name;

            bool actual = comparer.Equals(name, nameY);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_CaseAlternativeY_True()
        {
            string nameY = name.ToUpper();

            bool actual = comparer.Equals(name, nameY);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_DifferentNameY_False()
        {
            string nameY = name + "a";

            bool actual = comparer.Equals(name, nameY);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GetHashCode_SameHashCode_Equal()
        {
            int expected = name.GetHashCode();

            int actual = comparer.GetHashCode(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetHashCode_DifferentHashCode_NotEqual()
        {
            string name2 = name + "a";
            int expected = name.GetHashCode();

            int actual = comparer.GetHashCode(name2);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetHashCode_NullParameter_Throws()
        {
            comparer.GetHashCode(null);
        }
    }
}
