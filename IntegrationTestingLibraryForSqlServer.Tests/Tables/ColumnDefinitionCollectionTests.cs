using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ColumnDefinitionCollectionTests
    {
        private const string columnName = "c1";

        [Fact]
        public void GetName_Null_ReturnsNull()
        {
            string actual = ColumnDefinitionCollection.GetName(null);

            Assert.Null(actual);
        }

        [Fact]
        public void GetName_Column_ReturnsName()
        {
            string actual = ColumnDefinitionCollection.GetName(MockColumnDefinition.GetColumn(columnName));

            Assert.Equal(columnName, actual);
        }
    }
}
