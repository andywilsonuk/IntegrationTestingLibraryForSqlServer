using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionCollectionTests
    {
        private const string columnName = "c1";

        [TestMethod]
        public void GetName_Null_ReturnsNull()
        {
            string actual = ColumnDefinitionCollection.GetName(null);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetName_Column_ReturnsName()
        {
            string actual = ColumnDefinitionCollection.GetName(MockColumnDefinition.GetColumn(columnName));

            Assert.AreEqual(columnName, actual);
        }
    }
}
