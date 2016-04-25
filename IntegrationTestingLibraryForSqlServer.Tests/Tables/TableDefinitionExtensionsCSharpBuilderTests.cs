using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests.Tables
{
    [TestClass]
    public class TableDefinitionExtensionsCSharpBuilderTests
    {
        [TestMethod]
        public void TableDefinitionToCSharp()
        {
            var table = new TableDefinition(DatabaseObjectName.FromName("table1"), new[]
            {
                new ColumnDefinition { Name = "c1", DataType = SqlDbType.Int, AllowNulls = true },
                new ColumnDefinition { Name = "c2", DataType = SqlDbType.DateTime2, AllowNulls = false },
            });

            string actual = table.ToCSharp();

            string expected =
@"var table = new TableDefinition(new DatabaseObjectName(""[dbo].[table1]""), new [] {
    new ColumnDefinition { Name = ""c1"", DataType = SqlDbType.Int, AllowNulls = true },
    new ColumnDefinition { Name = ""c2"", DataType = SqlDbType.DateTime2, AllowNulls = false },
});";
            Assert.AreEqual(expected, actual);
        }
    }
}
