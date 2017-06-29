using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StandardColumnDefinition : ColumnDefinition
    {
        public StandardColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }
        protected override bool IsDataTypeAllowed => DataType.IsStandard;
    }
}
