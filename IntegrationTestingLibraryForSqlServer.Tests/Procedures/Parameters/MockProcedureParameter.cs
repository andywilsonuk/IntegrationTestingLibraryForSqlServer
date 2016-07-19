using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    internal class MockProcedureParameter : ProcedureParameter
    {
        public MockProcedureParameter(string name, SqlDbType dataType, ParameterDirection direction) 
            : base(name, dataType, direction)
        {
        }

        protected override bool IsDataTypeAllowed => true;

        public static MockProcedureParameter GetParameter(string name)
        {
            return new MockProcedureParameter(name, SqlDbType.Int, ParameterDirection.InputOutput);
        }
    }
}
