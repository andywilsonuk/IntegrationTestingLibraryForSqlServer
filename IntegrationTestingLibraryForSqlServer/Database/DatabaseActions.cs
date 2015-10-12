using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DatabaseActions
    {
        private string masterConnectionString;
        private string databaseName;

        public DatabaseActions(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            this.databaseName = builder.InitialCatalog;
            this.ConnectionString = connectionString;
            if (string.IsNullOrWhiteSpace(this.databaseName) || this.databaseName.Equals("master", StringComparison.CurrentCultureIgnoreCase)) throw new ArgumentException("The connection must have a database set", "connectionString");
            builder.InitialCatalog = "master";
            this.masterConnectionString = builder.ToString();
        }

        public string ConnectionString { get; private set; }

        public void Create()
        {
            using (SqlConnection connection = new SqlConnection(this.masterConnectionString))
            {
                connection.Execute(CreateDatabaseCommand, this.databaseName);
            }
        }

        public void Drop()
        {
            using (SqlConnection connection = new SqlConnection(this.masterConnectionString))
            {
                connection.Execute(DropDatabaseCommand, this.databaseName);
            }
        }

        public void CreateOrReplace()
        {
            this.Drop();
            this.Create();
        }

        public void GrantDomainUserAccess(string domain, string username)
        {
            if (string.IsNullOrWhiteSpace(domain)) throw new ArgumentNullException("domain");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException("username");

            string qualitiedName = domain + "\\" + username;

            if ((Environment.UserDomainName + "\\" + Environment.UserName).Equals(qualitiedName, StringComparison.CurrentCultureIgnoreCase))
                return;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Execute(AddWindowsUserCommand, qualitiedName);
                connection.Execute(GrantPermissions, qualitiedName);
            }
        }

        public void CreateSchema(string schemaName)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = string.Format(CreateSchemaScript, schemaName);
                    command.ExecuteNonQuery();
                }
            }
        }

        private const string CreateDatabaseCommand = @"create database [{0}]";
        private const string DropDatabaseCommand = @"
if exists(select name from sys.databases where name = '{0}')
begin
  alter database [{0}] set single_user with rollback immediate;
  drop database [{0}]
end";
        private const string AddWindowsUserCommand = @"
if not exists(select loginname from syslogins where loginname = '{0}') create login [{0}] from windows;";
        private const string GrantPermissions = @"
grant connect to [{0}]
alter role db_datareader add member [{0}]
alter role db_datawriter add member [{0}]
grant execute to [{0}]
";
        private const string CreateSchemaScript = @"
            IF NOT EXISTS(SELECT * FROM sys.schemas WHERE name = '{0}')
            BEGIN
                EXEC sp_executesql N'CREATE SCHEMA {0}'
            END";
    }
}