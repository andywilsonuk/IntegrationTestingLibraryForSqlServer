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
        private ProcedureParameterCollection parameters = new ProcedureParameterCollection();

        [TestMethod]
        public void AddFromRaw()
        {
            var expected = new IntegerProcedureParameter(ParameterName, SqlDbType.Int, ParameterDirection.InputOutput);
            var source = new[] { new ProcedureParameterRaw { Name = ParameterName, DataType = "Int", Direction = ParameterDirection.InputOutput } };

            parameters.AddFromRaw(source);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(expected, parameters[0]);
        }
        [TestMethod]
        public void AddBinary_Valid_Added()
        {
            var expected = new BinaryProcedureParameter(ParameterName, SqlDbType.Binary, ParameterDirection.InputOutput);

            var actual = parameters.AddBinary(ParameterName, SqlDbType.Binary);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddDecimal_Valid_Added()
        {
            var expected = new DecimalProcedureParameter(ParameterName, ParameterDirection.InputOutput);

            var actual = parameters.AddDecimal(ParameterName);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddInteger_Valid_Added()
        {
            var expected = new IntegerProcedureParameter(ParameterName, SqlDbType.Int, ParameterDirection.InputOutput);

            var actual = parameters.AddInteger(ParameterName, SqlDbType.Int);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddString_Valid_Added()
        {
            var expected = new StringProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.InputOutput);

            var actual = parameters.AddString(ParameterName, SqlDbType.VarChar);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddStandard_Valid_Added()
        {
            var expected = new StandardProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.InputOutput);

            var actual = parameters.AddStandard(ParameterName, SqlDbType.DateTime);

            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual(expected, actual);
        }
    }
}
