using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class ViewCheck
    {
        private string connectionString;

        public ViewCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public void VerifyMatch(TableBackedViewDefinition expected)
        //{
        //    expected.VerifyEqual(this.GetDefinition(expected.Name));
        //}

        //public void VerifyMatchOrSubset(TableBackedViewDefinition expected)
        //{
        //    expected.VerifyEqualOrSubsetOf(this.GetDefinition(expected.Name));
        //}

        //public TableBackedViewDefinition GetDefinition(string viewName)
        //{
        //    var mapper = new DataRecordToColumnMapper();

        //    using (SqlConnection connection = new SqlConnection(this.connectionString))
        //    {
        //        return new TableBackedViewDefinition(viewName,
        //            connection.Execute<ColumnDefinition>(
        //                (reader) => mapper.ToColumnDefinition(reader),
        //                ViewDefinitionQuery,
        //                viewName));
        //    }
        //}

        private const string ViewDefinitionQuery = @"SELECT TOP 1 * FROM [{0}]";
    }
}
