using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
