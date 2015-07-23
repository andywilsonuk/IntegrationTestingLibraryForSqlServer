using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal static class SqlConnectionExtensions
    {
        public static void Execute(this SqlConnection connection, string format, params object[] args)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = string.Format(format, args);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void ExecuteWithParameters(this SqlConnection connection, string commandText, IEnumerable<object> parameterValues)
        {
            var parameters = parameterValues.Select((x, i) => new SqlParameter("@" + i, x)).ToArray();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.Parameters.AddRange(parameters);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static IEnumerable<T> Execute<T>(this SqlConnection connection, Func<SqlDataReader, T> func, string format, params object[] args)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = string.Format(format, args);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return func(reader);
                    }
                }
                connection.Close();
            }
        }
    }
}
