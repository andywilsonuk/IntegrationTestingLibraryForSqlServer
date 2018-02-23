using System;
using System.Data.SqlClient;

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
            var masterBuilder = new SqlConnectionStringBuilder(builder.ToString())
            {
                InitialCatalog = MasterCatalog
            };
            return masterBuilder;
        }
    }
}
