using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public interface TableDataValueTransformer
    {
        object Transform(object value);
    }
}
