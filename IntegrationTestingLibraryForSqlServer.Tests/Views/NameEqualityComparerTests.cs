using System;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class NameEqualityComparerTests
    {
        private NameEqualityComparer<string> comparer = new NameEqualityComparer<string>(input => input);
        private const string name = "@p1";

        [Fact]
        public void Equals_NullX_False()
        {
            bool actual = comparer.Equals(null, name);

            Assert.False(actual);
        }

        [Fact]
        public void Equals_NullY_False()
        {
            bool actual = comparer.Equals(name, null);

            Assert.False(actual);
        }

        [Fact]
        public void Equals_NullXY_True()
        {
            bool actual = comparer.Equals(null, null);

            Assert.True(actual);
        }

        [Fact]
        public void Equals_SameXY_True()
        {
            bool actual = comparer.Equals(name, name);

            Assert.True(actual);
        }

        [Fact]
        public void Equals_EquivalentY_True()
        {
            string nameY = name;

            bool actual = comparer.Equals(name, nameY);

            Assert.True(actual);
        }

        [Fact]
        public void Equals_CaseAlternativeY_True()
        {
            string nameY = name.ToUpper();

            bool actual = comparer.Equals(name, nameY);

            Assert.True(actual);
        }

        [Fact]
        public void Equals_DifferentNameY_False()
        {
            string nameY = name + "a";

            bool actual = comparer.Equals(name, nameY);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCode_SameHashCode_Equal()
        {
            int expected = name.GetHashCode();

            int actual = comparer.GetHashCode(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetHashCode_DifferentHashCode_NotEqual()
        {
            string name2 = name + "a";
            int expected = name.GetHashCode();

            int actual = comparer.GetHashCode(name2);

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void GetHashCode_NullParameter_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => comparer.GetHashCode(null));
        }
    }
}
