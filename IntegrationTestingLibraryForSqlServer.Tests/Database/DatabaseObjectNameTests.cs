using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DatabaseObjectNameTests
    {
        private static string qualified = "[s1].[t1]";

        [TestMethod]
        public void NoBrackets()
        {
            var objectName = new DatabaseObjectName("s1", "t1");

            Assert.AreEqual(qualified, objectName.Qualified);
            Assert.AreEqual("[s1]", objectName.SchemaName);
            Assert.AreEqual("[t1]", objectName.ObjectName);
        }
        [TestMethod]
        public void WithBrackets()
        {
            var objectName = new DatabaseObjectName("[s1]", "[t1]");

            Assert.AreEqual(qualified, objectName.Qualified);
        }
        [TestMethod]
        public void MissingSchema()
        {
            var objectName = new DatabaseObjectName(null, "t1");

            Assert.AreEqual("[dbo].[t1]", objectName.Qualified);
            Assert.AreEqual("[dbo]", objectName.SchemaName);
            Assert.AreEqual("[t1]", objectName.ObjectName);
        }
        [TestMethod]
        public void QualifiedToString()
        {
            var objectName = new DatabaseObjectName("[s1]", "[t1]");

            Assert.AreEqual(qualified, objectName.ToString());
        }
        [TestMethod]
        public void FromNameWithSchema()
        {
            var objectName = DatabaseObjectName.FromName(qualified);

            Assert.AreEqual(qualified, objectName.Qualified);
            Assert.AreEqual("[s1]", objectName.SchemaName);
            Assert.AreEqual("[t1]", objectName.ObjectName);
        }
        [TestMethod]
        public void FromNameWithoutSchema()
        {
            var objectName = DatabaseObjectName.FromName("t1");

            Assert.AreEqual("[dbo]", objectName.SchemaName);
            Assert.AreEqual("[t1]", objectName.ObjectName);
        }
        [TestMethod]
        public void HashCodeSame()
        {
            var objectName1 = new DatabaseObjectName("s1", "t1");
            var objectName2 = new DatabaseObjectName("s1", "t1");

            Assert.AreEqual(objectName1.GetHashCode(), objectName2.GetHashCode());
        }
        [TestMethod]
        public void HashCodeSameMixedCase()
        {
            var objectName1 = new DatabaseObjectName("s1", "T1");
            var objectName2 = new DatabaseObjectName("S1", "t1");

            Assert.AreEqual(objectName1.GetHashCode(), objectName2.GetHashCode());
        }
        [TestMethod]
        public void HashCodeDifferent()
        {
            var objectName1 = new DatabaseObjectName("s1", "t2");
            var objectName2 = new DatabaseObjectName("s1", "t1");

            Assert.AreNotEqual(objectName1.GetHashCode(), objectName2.GetHashCode());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromNameNull()
        {
            DatabaseObjectName.FromName(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNullObjectName()
        {
            new DatabaseObjectName("dbo", null);
        }
    }
}

