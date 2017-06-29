using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    internal class MockVariableSizeProcedureParameter : VariableSizeProcedureParameter
    {
        public MockVariableSizeProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => true;

        public static MockVariableSizeProcedureParameter GetParameter(string name)
        {
            return new MockVariableSizeProcedureParameter(name, SqlDbType.VarChar, ParameterDirection.InputOutput);
        }
    }
}
