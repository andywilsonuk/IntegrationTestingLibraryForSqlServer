using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterCollectionExtensionsTests
    {
        [TestMethod]
        public void AddFromRaw()
        {
            var source = new[] { new ProcedureParameterRaw { Name = "C1", DataType = "Int" } };
            ICollection<ProcedureParameter> parameters = new Collection<ProcedureParameter>();

            parameters.AddFromRaw(source);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(source[0].Name, parameters.First().Name);
        }
    }
}
