using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataValueComparerPipeElement
    {
        void Process(TableDataValueComparerPipeElementArguments args);
    }
}
