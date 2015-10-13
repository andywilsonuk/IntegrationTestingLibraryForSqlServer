using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataGuidValueComparerTests
    {
        private TableDataGuidValueComparer comparer = new TableDataGuidValueComparer();

        [TestMethod]
        public void GuidXGuidYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), Y = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}") };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void GuidXGuidYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{3604134B-8010-4CF4-ADDA-9130D10168AE}"), Y = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}") };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXGuidY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}", Y = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}") };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void GuidXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), Y = "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "23CB5003-ABDF-4CA3-B7F7-EA707D06DE40", Y = "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void GuidXIntY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), Y = 5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [TestMethod]
        public void TableDataGuidAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
