using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StringProcedureParameter : VariableSizeProcedureParameter
    {
        public StringProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsString;
    }
}
