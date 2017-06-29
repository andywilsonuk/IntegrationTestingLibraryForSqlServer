using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterTests
    {
        private const string ParameterName = "p1";
        private const string QualifiedParameterName = "@" + ParameterName;

        [TestMethod]
        public void Constructor_PassedValues_SetAgainstProperties()
        {
            var parameter = GetParameter(ParameterName, SqlDbType.DateTime);

            Assert.AreEqual(SqlDbType.DateTime, parameter.DataType.SqlType);
            Assert.AreEqual(ParameterDirection.Input, parameter.Direction);
        }

        [TestMethod]
        public void Name_NotQualified_True()
        {
            var parameter = GetParameter(ParameterName, SqlDbType.DateTime);

            Assert.AreEqual(QualifiedParameterName, parameter.Name);
        }

        [TestMethod]
        public void Name_AlreadyQualified_True()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);

            Assert.AreEqual(QualifiedParameterName, parameter.Name);
        }

        [TestMethod]
        public void Equals_Equivalent_True()
        {
            var x = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            var y = GetParameter(QualifiedParameterName, SqlDbType.DateTime);

            bool actual = x.Equals(y);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_Null_False()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);

            bool actual = parameter.Equals(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_DifferentName_False()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            var other = GetParameter("other", SqlDbType.DateTime);

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_DifferentDataType_False()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            var other = GetParameter(QualifiedParameterName, SqlDbType.SmallDateTime);

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_DifferentDirection_False()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            var other = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            other.Direction = ParameterDirection.Output;

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_EquivalentDirectionsLeft_True()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            parameter.Direction = ParameterDirection.InputOutput;
            var other = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            other.Direction = ParameterDirection.Output;

            bool actual = parameter.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_EquivalentDirectionsRight_True()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            parameter.Direction = ParameterDirection.Output;
            var other = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            other.Direction = ParameterDirection.InputOutput;

            bool actual = parameter.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GetHashCode_Lowered_Equal()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            int expected = QualifiedParameterName.GetHashCode();

            int actual = parameter.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetHashCode_Qualified_Equal()
        {
            var expected = GetParameter(QualifiedParameterName, SqlDbType.Date);

            var actual = GetParameter(ParameterName, SqlDbType.Date);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_Overridden_Equal()
        {
            var parameter = GetParameter(QualifiedParameterName, SqlDbType.DateTime);
            string expected = "Name: " + QualifiedParameterName + ", Data type: DateTime, Direction: Input";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }

        private ProcedureParameter GetParameter(string name, SqlDbType type)
        {
            return new MockProcedureParameter(name, type, ParameterDirection.Input);
        }
    }
}
