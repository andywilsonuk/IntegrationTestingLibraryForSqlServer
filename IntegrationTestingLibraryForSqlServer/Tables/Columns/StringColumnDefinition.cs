using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StringColumnDefinition : VariableSizeColumnDefinition
    {
        public StringColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsString;
    }
}
