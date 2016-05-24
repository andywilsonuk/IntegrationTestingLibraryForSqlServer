using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    internal class MockVariableSizeProcedureParameter : VariableSizeProcedureParameter
    {
        public MockVariableSizeProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => true;
    }
}
