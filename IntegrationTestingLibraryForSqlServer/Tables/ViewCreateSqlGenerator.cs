﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ViewCreateSqlGenerator
    {
        public string Sql(ViewDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException("defintion");
            return string.Format(CreateViewFormat, definition.Name, definition.BackingTable);
        }

        private const string CreateViewFormat = "CREATE VIEW [{0}] AS SELECT * FROM [{1}]";
    }
}