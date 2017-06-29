using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataCaseSensitiveStringValueComparerTests
    {
        private TableDataCaseSensitiveStringValueComparer comparer;

        [TestInitialize]
        public void TestInitialize()
        {
            comparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [TestMethod]
        public void TableDataCaseSensitiveStringValueComparerIsMatchTrue()
        {
            string x = "a";
            string y = "a";

            bool actual = comparer.IsMatch(x, y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataCaseSensitiveStringValueComparerIsMatchTrueForInteger()
        {
            int x = 1;
            string y = "1";

            bool actual = comparer.IsMatch(x, y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataCaseSensitiveStringValueComparerIsMatchFalse()
        {
            string x = "a";
            string y = "b";

            bool actual = comparer.IsMatch(x, y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataCaseSensitiveStringValueComparerIsMatchFalseNullX()
        {
            string x = null;
            string y = "b";

            bool actual = comparer.IsMatch(x, y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataCaseSensitiveStringValueComparerIsMatchFalseNullY()
        {
            string x = "a";
            string y = null;

            bool actual = comparer.IsMatch(x, y);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataCaseSensitiveStringValueComparerIsMatchTrueBothNull()
        {
            string x = null;
            string y = null;

            bool actual = comparer.IsMatch(x, y);

            Assert.IsTrue(actual);
        }
    }
}
