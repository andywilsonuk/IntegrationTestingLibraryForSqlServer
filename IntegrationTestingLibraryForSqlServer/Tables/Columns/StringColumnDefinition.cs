using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
