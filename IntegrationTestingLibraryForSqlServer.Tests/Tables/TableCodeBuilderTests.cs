using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableCodeBuilderTests
    {
        [TestMethod]
        public void TableDefinitionToCSharp()
        {
            var table = new TableDefinition(DatabaseObjectName.FromName("table1"), new ColumnDefinition[]
            {
                new IntegerColumnDefinition("c1", SqlDbType.Int) { AllowNulls = true },
                new StandardColumnDefinition("c2", SqlDbType.DateTime2) { AllowNulls = false },
                new SizeableColumnDefinition("c3", SqlDbType.VarChar) { AllowNulls = false, Size = 100 },
                new DecimalColumnDefinition("c4") { AllowNulls = false, Precision = 10, Scale = 2 },
                new IntegerColumnDefinition("c1", SqlDbType.Int) { IdentitySeed = 1 },
            });

            string actual = table.ToCSharp();

            string expected =
@"var table = new TableDefinition(new DatabaseObjectName(""[dbo].[table1]""), new [] {
    new IntegerColumnDefinition(""c1"", SqlDbType.Int) { AllowNulls = true },
    new StandardColumnDefinition(""c2"", SqlDbType.DateTime2) { AllowNulls = false },
    new SizeableColumnDefinition(""c3"", SqlDbType.VarChar) { AllowNulls = false, Size = 100 },
    new DecimalColumnDefinition(""c4"") { AllowNulls = false, Precision = 10, Scale = 2 },
    new IntegerColumnDefinition(""c1"", SqlDbType.Int) { AllowNulls = false, IdentitySeed = 1 },
});";
            Assert.AreEqual(expected, actual);
        }
    }
}
