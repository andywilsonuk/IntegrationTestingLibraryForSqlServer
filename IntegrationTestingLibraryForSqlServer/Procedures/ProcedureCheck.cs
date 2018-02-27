using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureCheck
    {
        private const string derviceParameters = @"
SELECT  
    pa.name,
    ty.name,
    pa.max_length,
    pa.precision,
    pa.scale,
    pa.is_output
FROM sys.procedures pr
JOIN sys.parameters pa ON pa.object_id = pr.object_id
JOIN sys.types ty ON ty.user_type_id = pa.user_type_id
WHERE pr.name = '{0}'
ORDER BY pa.parameter_id";
        private string connectionString;

        public ProcedureCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void VerifyMatch(ProcedureDefinition expected)
        {
            var actual = GetDefinition(expected.Name);
            expected.VerifyEqual(actual);
        }

        public ProcedureDefinition GetDefinition(DatabaseObjectName name)
        {
            var parameters = DeriveParameters(name);
            return new ProcedureDefinition(name, parameters);
        }

#if NETSTANDARD2_0
        ///<remarks>Adapted from https://stackoverflow.com/questions/44301937/is-there-any-way-to-derive-parameters-of-a-stored-procedure-in-net-core</remarks>
        private IEnumerable<ProcedureParameter> DeriveParameters(DatabaseObjectName name)
        {
            var mapper = new ProcedureParameterFactory();
            string sql = string.Format(derviceParameters, name.ObjectNameWithoutBrackets);

            using (var connection = new SqlConnection(connectionString))
            {
                var rawItems = connection.Execute(reader =>
                {
                    var dataType = new DataType(reader.GetString(1));

                    return new ProcedureParameterRaw
                    {
                        Name = reader.GetString(0),
                        DataType = reader.GetString(1),
                        Size = dataType.IsDecimal ? reader.GetByte(3) : dataType.IsUnicodeString ? reader.GetInt16(2) / 2 : reader.GetInt16(2),
                        DecimalPlaces = reader.GetByte(4),
                        Direction = reader.GetBoolean(5) ? ParameterDirection.InputOutput : ParameterDirection.Input,
                    };
                }, sql, name.ObjectName).ToList();

                return mapper.FromRaw(rawItems);
            }
        }
#else
        private IEnumerable<ProcedureParameter> DeriveParameters(DatabaseObjectName name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = name.Qualified;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    SqlCommandBuilder.DeriveParameters(command);
                    var mapper = new SqlParameterToProcedureParameterMapper();
                    return command.Parameters.Cast<SqlParameter>().Select(x => mapper.FromSqlParameter(x));
                }
            }
        }
#endif
    }
}
