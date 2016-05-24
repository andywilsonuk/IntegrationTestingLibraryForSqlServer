using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    internal class MockVariableSizeColumnDefinition : VariableSizeColumnDefinition
    {
        public MockVariableSizeColumnDefinition(string name, SqlDbType dataType) 
            : base(name, dataType)
        {
        }

        protected override bool IsDataTypeAllowed => true;
    }
}
