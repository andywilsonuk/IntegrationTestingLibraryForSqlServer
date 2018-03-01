using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StringColumnDefinition : VariableSizeColumnDefinition
    {
        public StringColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }

        public StringColumnDefinition(string name, SqlDbType dataType, int size)
            : base(name, dataType)
        {
            Size = size;
        }

        protected override bool IsDataTypeAllowed => DataType.IsString;
    }
}
