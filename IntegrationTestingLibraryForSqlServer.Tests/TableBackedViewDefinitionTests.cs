using System;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableBackedViewDefinitionTests
    {
        [Fact]
        public void TableBackedViewDefinitionNullName()
        {
            Assert.Throws<ArgumentNullException>(() => new TableBackedViewDefinition(null, DatabaseObjectName.FromName("t1")));
        }

        [Fact]
        public void TableBackedViewDefinitionNullTableName()
        {
            Assert.Throws<ArgumentNullException>(() => new TableBackedViewDefinition(DatabaseObjectName.FromName("v1"), null));
        }

        [Fact]
        public void TableBackedViewDefinitionPropertiesSet()
        {
            var definition = new TableBackedViewDefinition(DatabaseObjectName.FromName("v1"), DatabaseObjectName.FromName("t1"));

            Assert.Equal("[dbo].[v1]", definition.Name.Qualified);
            Assert.Equal("[dbo].[t1]", definition.BackingTable.Qualified);
        }
    }
}
