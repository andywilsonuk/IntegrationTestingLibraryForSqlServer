using System;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DatabaseObjectNameTests
    {
        private static string qualified = "[s1].[t1]";

        [Fact]
        public void NoBrackets()
        {
            var objectName = new DatabaseObjectName("s1", "t1");

            Assert.Equal(qualified, objectName.Qualified);
            Assert.Equal("[s1]", objectName.SchemaName);
            Assert.Equal("[t1]", objectName.ObjectName);
        }
        [Fact]
        public void WithBrackets()
        {
            var objectName = new DatabaseObjectName("[s1]", "[t1]");

            Assert.Equal(qualified, objectName.Qualified);
        }
        [Fact]
        public void MissingSchema()
        {
            var objectName = new DatabaseObjectName(null, "t1");

            Assert.Equal("[dbo].[t1]", objectName.Qualified);
            Assert.Equal("[dbo]", objectName.SchemaName);
            Assert.Equal("[t1]", objectName.ObjectName);
        }
        [Fact]
        public void QualifiedToString()
        {
            var objectName = new DatabaseObjectName("[s1]", "[t1]");

            Assert.Equal(qualified, objectName.ToString());
        }
        [Fact]
        public void FromNameWithSchema()
        {
            var objectName = DatabaseObjectName.FromName(qualified);

            Assert.Equal(qualified, objectName.Qualified);
            Assert.Equal("[s1]", objectName.SchemaName);
            Assert.Equal("[t1]", objectName.ObjectName);
        }
        [Fact]
        public void FromNameWithoutSchema()
        {
            var objectName = DatabaseObjectName.FromName("t1");

            Assert.Equal("[dbo]", objectName.SchemaName);
            Assert.Equal("[t1]", objectName.ObjectName);
        }
        [Fact]
        public void HashCodeSame()
        {
            var objectName1 = new DatabaseObjectName("s1", "t1");
            var objectName2 = new DatabaseObjectName("s1", "t1");

            Assert.Equal(objectName1.GetHashCode(), objectName2.GetHashCode());
        }
        [Fact]
        public void HashCodeSameMixedCase()
        {
            var objectName1 = new DatabaseObjectName("s1", "T1");
            var objectName2 = new DatabaseObjectName("S1", "t1");

            Assert.Equal(objectName1.GetHashCode(), objectName2.GetHashCode());
        }
        [Fact]
        public void HashCodeDifferent()
        {
            var objectName1 = new DatabaseObjectName("s1", "t2");
            var objectName2 = new DatabaseObjectName("s1", "t1");

            Assert.NotEqual(objectName1.GetHashCode(), objectName2.GetHashCode());
        }
        [Fact]
        public void FromNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => DatabaseObjectName.FromName(null));
        }
        [Fact]
        public void ConstructorNullObjectName()
        {
            Assert.Throws<ArgumentException>(() => new DatabaseObjectName("dbo", null));
        }
    }
}

