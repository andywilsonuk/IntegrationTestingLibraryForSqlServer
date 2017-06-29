using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class BinaryProcedureParameter : VariableSizeProcedureParameter
    {
        public BinaryProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsBinary;
    }
}
