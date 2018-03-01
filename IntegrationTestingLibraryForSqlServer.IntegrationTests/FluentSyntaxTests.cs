using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    public class FluentSyntaxTests
    {
        [Fact]
        public void Creating_Objects_With_Fluent_Syntax()
        {
            string connectionString = $@"server=(localdb)\MSSQLLocalDB;database={Guid.NewGuid()};integrated security=True";
            var expected = TableData.FromRows(new[]
            {
                new List<object> { "bb", 20, 10 },
            });
            var table = new TableCheck(connectionString);
            var database = FakeSql.CreateDatabase(connectionString);
            database.CreateTable(
                    new TableDefinition("t1", new ColumnDefinition[]
                    {
                        new StringColumnDefinition("c1", SqlDbType.NVarChar, 50)
                        {
                            AllowNulls = false
                        },
                        new IntegerColumnDefinition("c2", SqlDbType.Int),
                        new DecimalColumnDefinition("c3", 10, 5),
                    }))
                    .InsertRow("aa", 25, 3.5)
                    .InsertRow("bb", 20, 10);

            var actual = table.GetData("t1");

            expected.VerifyMatch(actual, TableDataComparers.SubsetRowOrdinalColumn);

            database.Drop();
        }
    }
}
