using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class IntegerProcedureParameter : ProcedureParameter
    {
        public IntegerProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsInteger;
    }
}
