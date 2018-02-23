using System;
using Xunit;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableInsertSqlGeneratorTests
    {
        private DatabaseObjectName tableName = DatabaseObjectName.FromName("t1");
        private List<string> columnNames = new List<string> { "c1" };
        private TableInsertSqlGenerator generator = new TableInsertSqlGenerator();

        [Fact]
        public void InsertTableNullNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => generator.Sql(null, columnNames));
        }

        [Fact]
        public void InsertTable()
        {
            string expected = string.Format("INSERT INTO {0} (c1) SELECT @0", tableName.Qualified);

            string actual = generator.Sql(tableName, columnNames);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InsertTableNullColumnNames()
        {
            Assert.Throws<ArgumentNullException>(() => generator.Sql(tableName, null));
        }

        [Fact]
        public void InsertTableMultipleColumns()
        {
            string expected = string.Format("INSERT INTO {0} (c1,c2) SELECT @0,@1", tableName.Qualified);
            columnNames.Add("c2");

            string actual = generator.Sql(tableName, columnNames);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InsertTableNoColumnNamesNullNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => generator.Sql(null, 2));
        }

        [Fact]
        public void InsertTableNoColumnNamesInvalidCountThrowsException()
        {
            Assert.Throws<ArgumentException>(() => generator.Sql(tableName, -1));
        }

        [Fact]
        public void InsertTableNoColumnNames()
        {
            string expected = string.Format("INSERT INTO {0} SELECT @0", tableName.Qualified);

            string actual = generator.Sql(tableName, 1);

            Assert.Equal(expected, actual);
        }
    }
}
