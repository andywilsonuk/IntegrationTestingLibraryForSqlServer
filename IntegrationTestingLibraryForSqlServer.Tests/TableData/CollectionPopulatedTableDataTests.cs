using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class CollectionPopulatedTableDataTests
    {
        private List<string> columnNames;

        public CollectionPopulatedTableDataTests()
        {
            columnNames = new List<string> { "a", "b" };
        }

        [Fact]
        public void CollectionPopulatedTableDataStringValues()
        {
            var expectedColumnNames = new List<string> { "a", "b" };
            var expectedRows = new List<IList<object>> { new List<object> { "1", "2" } };
            var sourceRows = new List<List<string>> { new List<string> { "1", "2" } };

            var tableData = new CollectionPopulatedTableData(columnNames, sourceRows);

            Assert.True(expectedColumnNames.SequenceEqual(tableData.ColumnNames));
            Assert.True(expectedRows[0].SequenceEqual(tableData.Rows[0]));
        }

        [Fact]
        public void CollectionPopulatedTableDataObjectValues()
        {
            var expectedColumnNames = new List<string> { "a", "b" };
            var expectedRows = new List<IList<object>> { new List<object> { "1", "2" } };
            var sourceRows = new List<List<object>> { new List<object> { "1", "2" } };

            var tableData = new CollectionPopulatedTableData(columnNames, sourceRows);

            Assert.True(expectedColumnNames.SequenceEqual(tableData.ColumnNames));
            Assert.True(expectedRows[0].SequenceEqual(tableData.Rows[0]));
        }

        [Fact]
        public void CollectionPopulatedWithStringDBNullConvertedToDBNull()
        {
            var expectedRows = new List<IList<object>> { new List<object> { "1", null } };
            var sourceRows = new List<List<object>> { new List<object> { "1", "DBNull" } };

            var tableData = new CollectionPopulatedTableData(columnNames, sourceRows);

            Assert.True(expectedRows[0].SequenceEqual(tableData.Rows[0]));
        }
    }
}
