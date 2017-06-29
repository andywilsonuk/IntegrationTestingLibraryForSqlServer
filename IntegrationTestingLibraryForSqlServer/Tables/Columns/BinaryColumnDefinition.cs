using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class BinaryColumnDefinition : VariableSizeColumnDefinition
    {
        public BinaryColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }

        protected override bool IsDataTypeAllowed => DataType.IsBinary;
    }
}
