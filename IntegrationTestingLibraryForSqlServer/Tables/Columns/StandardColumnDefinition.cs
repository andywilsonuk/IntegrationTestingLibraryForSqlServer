using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StandardColumnDefinition : ColumnDefinition
    {
        public StandardColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
            if (!DataType.IsInteger && !DataType.IsDecimal && !DataType.IsSizeable) return;
            throw new ArgumentException("Wrong datatype passed", nameof(dataType));
        }
    }
}
