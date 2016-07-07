using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal static class SqlConnectionStringBuilderExtensions
    {
        private const string MasterCatalog = "master";

        public static bool IsMasterCatalog(this SqlConnectionStringBuilder builder)
        {
            return string.Equals(builder.InitialCatalog, MasterCatalog, StringComparison.CurrentCultureIgnoreCase);
        }

        public static SqlConnectionStringBuilder ToMasterCatalog(this SqlConnectionStringBuilder builder)
        {
            var masterBuilder = new SqlConnectionStringBuilder(builder.ToString());
            masterBuilder.InitialCatalog = MasterCatalog;
            return masterBuilder;
        }
    }
}
