using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    internal class MockColumnDefinition : ColumnDefinition
    {
        public MockColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }

        protected override bool IsDataTypeAllowed => true;

        public static MockColumnDefinition GetColumn(string name)
        {
            return new MockColumnDefinition(name, SqlDbType.Int);
        }
    }
}
