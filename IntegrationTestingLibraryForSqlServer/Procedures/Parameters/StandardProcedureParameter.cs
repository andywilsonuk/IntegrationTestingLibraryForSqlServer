using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class StandardProcedureParameter : ProcedureParameter
    {
        public StandardProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
            if (!DataType.IsDecimal && !DataType.IsSizeable) return;
            throw new ArgumentException("Wrong datatype passed", nameof(dataType));
        }
    }
}
