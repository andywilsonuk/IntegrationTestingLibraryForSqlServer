using System;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableBackedViewCreateSqlGeneratorTests
    {
        TableBackedViewCreateSqlGenerator generator = new TableBackedViewCreateSqlGenerator();

        [Fact]
        public void CreateTableNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => generator.Sql(null));
        }

        [Fact]
        public void CreateView()
        {
            var viewDefinition = new TableBackedViewDefinition(DatabaseObjectName.FromName("v1"), DatabaseObjectName.FromName("t1"));
            string expected = string.Format("CREATE VIEW [dbo].[v1] AS SELECT * FROM [dbo].[t1]");

            string actual = generator.Sql(viewDefinition);
             
            Assert.Equal(expected, actual);
        }
    }
}
