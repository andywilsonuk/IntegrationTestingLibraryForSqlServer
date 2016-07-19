using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterCollectionExtensionsTests
    {
        private const string ParameterName = "@p1";

        [TestMethod]
        public void AddFromRaw()
        {
            var source = new[] { new ProcedureParameterRaw { Name = ParameterName, DataType = "Int" } };
            ICollection<ProcedureParameter> parameters = new Collection<ProcedureParameter>();

            parameters.AddFromRaw(source);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(source[0].Name, parameters.First().Name);
        }

        [TestMethod]
        public void AddBinary_Valid_Added()
        {
            var parameters = new ProcedureParameterCollection();

            var parameter = parameters.AddBinary(ParameterName, SqlDbType.Binary);

            Assert.AreEqual(1, parameters.Count);
            Assert.IsNotNull(parameter);
            Assert.AreEqual(ParameterName, parameter.Name);
        }
    }
}
