using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StandardProcedureParameter : ProcedureParameter
    {
        public StandardProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsStandard;
    }
}
