using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    internal class MockVariableSizeColumnDefinition : VariableSizeColumnDefinition
    {
        public MockVariableSizeColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }

        protected override bool IsDataTypeAllowed => true;

        public static MockVariableSizeColumnDefinition GetColumn(string name)
        {
            return new MockVariableSizeColumnDefinition(name, SqlDbType.VarChar);
        }
    }
}
