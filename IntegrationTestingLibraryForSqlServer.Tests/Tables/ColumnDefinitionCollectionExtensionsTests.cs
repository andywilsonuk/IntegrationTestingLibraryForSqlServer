using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionCollectionExtensionsTests
    {
        [TestMethod]
        public void AddFromRaw()
        {
            var source = new[] { new ColumnDefinitionRaw { Name = "C1", DataType = "Int" } };
            ICollection<ColumnDefinition> columns = new Collection<ColumnDefinition>();

            columns.AddFromRaw(source);

            Assert.AreEqual(1, columns.Count);
            Assert.AreEqual(source[0].Name, columns.First().Name);
        }
    }
}
