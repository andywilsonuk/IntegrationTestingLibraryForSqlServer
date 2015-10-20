using System;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableDataNullValueTransformer : TableDataValueTransformer
    {
        private const string NullString = "NULL";

        public object Transform(object value)
        {
            if (value is string)
                if ((string)value == NullString)
                    return DBNull.Value;

            return value;
        }
    }
}