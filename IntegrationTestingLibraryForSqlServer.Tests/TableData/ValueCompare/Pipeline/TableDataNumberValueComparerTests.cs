using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataNumberValueComparerTests
    {
        private TableDataNumberValueComparer comparer = new TableDataNumberValueComparer();

        [TestMethod]
        public void DecimalXDecimalYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = 5.5m };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXDecimalYMatchingPadded()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = 5.50m };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXDecimalYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = 10m };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXDecimalY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "5.5", Y = 5.5m };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXDecimalYPadded()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "05.50", Y = 5.5m };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = "5.5" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "5.5", Y = "5.5" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXBoolY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = false };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [TestMethod]
        public void TableDataDecimalAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXIntYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXUIntYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5u };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXFloatYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5f };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXDoubleYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5d };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXLongYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5L };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXULongYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5UL };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXSByteYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = (sbyte)5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXShortYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = (short)5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DecimalXUShortYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = (ushort)5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }
    }
}
