using System;

namespace IntegrationTestingLibraryForSqlServer
{
    public class SqlAccount
    {
        public SqlAccount(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}
