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
                new StringColumnDefinition("c3", SqlDbType.VarChar) { AllowNulls = false, Size = 100 },
                new DecimalColumnDefinition("c4") { AllowNulls = false, Precision = 10, Scale = 2 },
                new IntegerColumnDefinition("c5", SqlDbType.Int) { IdentitySeed = 1 },
                new BinaryColumnDefinition("c6", SqlDbType.VarBinary) { AllowNulls = true, Size = 1000 },
            });

            string actual = table.ToCSharp();

            string expected =
@"var table = new TableDefinition(DatabaseObjectName.FromName(""[dbo].[table1]""), new ColumnDefinition[] {
    new IntegerColumnDefinition(""c1"", SqlDbType.Int) { AllowNulls = true },
    new StandardColumnDefinition(""c2"", SqlDbType.DateTime2) { AllowNulls = false },
    new StringColumnDefinition(""c3"", SqlDbType.VarChar) { AllowNulls = false, Size = 100 },
    new DecimalColumnDefinition(""c4"") { AllowNulls = false, Precision = 10, Scale = 2 },
    new IntegerColumnDefinition(""c5"", SqlDbType.Int) { AllowNulls = false, IdentitySeed = 1 },
    new BinaryColumnDefinition(""c6"", SqlDbType.VarBinary) { AllowNulls = true, Size = 1000 },
});";
            // GitHub stores this source file with \n line breaks not \r\n so update it to use the windows format
            expected = expected.Replace("\n", Environment.NewLine);

            Assert.AreEqual(expected, actual);
        }
    }
}
