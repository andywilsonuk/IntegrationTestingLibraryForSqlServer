using System;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DatabaseActions
    {
        private SqlConnectionStringBuilder connectionDetails;
        private SqlConnectionStringBuilder masterConnectionDetails;
        public DatabaseActions(string connectionString)
        {
            connectionDetails = new SqlConnectionStringBuilder(connectionString);
            if (string.IsNullOrWhiteSpace(connectionDetails.InitialCatalog) || connectionDetails.IsMasterCatalog())
                throw new ArgumentException("The connection must have an initial catalog set", "connectionString");
            masterConnectionDetails = connectionDetails.ToMasterCatalog();
        }

        public DatabaseActions(string connectionString, string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName)) throw new ArgumentException("Database name cannot be blank");
            connectionDetails = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = databaseName,
            };
            masterConnectionDetails = connectionDetails.ToMasterCatalog();
        }

        public string ConnectionString
        {
            get { return connectionDetails.ToString(); }
        }

        public bool IsLocalDB
        {
            get { return connectionDetails.DataSource.Contains("(localdb)"); }
        }

        public string Name
        {
            get { return connectionDetails.InitialCatalog; }
        }

        public void Create()
        {
            using (var connection = new SqlConnection(masterConnectionDetails.ToString()))
            {
                connection.Execute(CreateDatabaseCommand, connectionDetails.InitialCatalog);
            }
        }

        public void Drop()
        {
            using (var connection = new SqlConnection(masterConnectionDetails.ToString()))
            {
                connection.Execute(DropDatabaseCommand, connectionDetails.InitialCatalog);
            }
        }

        public void CreateOrReplace()
        {
            Drop();
            Create();
        }

        public void GrantUserAccess(DomainAccount account)
        {
            if (account == null) throw new ArgumentNullException("account");
            if (new DomainAccount(Environment.UserDomainName, Environment.UserName).Equals(account)) return;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(AddDomainUserCommand, account.Qualified);
                connection.Execute(GrantPermissions, account.Qualified);
            }
        }

        public void GrantUserAccess(SqlAccount account)
        {
            if (account == null) throw new ArgumentNullException("account");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(AddSqlUserCommand, account.Username, account.Password);
                connection.Execute(GrantPermissions, account.Username);
            }
        }

        public void CreateSchema(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(CreateSchemaCommand, name);
            }
        }

        private const string CreateDatabaseCommand = @"create database [{0}]";
        private const string DropDatabaseCommand = @"
            if exists(select name from sys.databases where name = '{0}')
            begin
              alter database [{0}] set single_user with rollback immediate;
              drop database [{0}]
            end";
        private const string AddDomainUserCommand = @"
            if not exists(select loginname from syslogins where loginname = '{0}') create login [{0}] from windows;";
        private const string AddSqlUserCommand = @"
            if not exists(select loginname from syslogins where loginname = '{0}') create login [{0}] WITH password=N'{1}';";
        private const string GrantPermissions = @"
            create user [{0}] for login [{0}]
            grant connect to [{0}]
            alter role db_datareader add member [{0}]
            alter role db_datawriter add member [{0}]
            grant execute to [{0}]";
        private const string CreateSchemaCommand = @"
            if not exists(select name from sys.schemas where name = '{0}')
            begin
                exec sp_executesql N'create schema [{0}]'
            end";
    }
}
