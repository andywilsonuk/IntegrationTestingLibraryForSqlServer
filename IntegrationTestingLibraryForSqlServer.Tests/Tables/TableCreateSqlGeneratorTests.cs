using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableCreateSqlGeneratorTests
    {
        TableCreateSqlGenerator generator = new TableCreateSqlGenerator();
        TableDefinition definition = new TableDefinition("t1");
        private const string TEST_SCHEMA = "testSchema";
        TableDefinition definitionWithSchema = new TableDefinition("t1", TEST_SCHEMA);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableNullThrowsException()
        {
            generator.Sql(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableNullSchemaThrowsException()
        {
            TableDefinition definitionWithNullSchema = new TableDefinition("t1", null);
        }

        [TestMethod]
        public void CreateTable()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] Decimal(10,5) NOT NULL)", Constants.DEFAULT_SCHEMA);
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Decimal, Size = 10, DecimalPlaces = 5, AllowNulls = false });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableInSchema()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] Decimal(10,5) NOT NULL)", TEST_SCHEMA);
            definitionWithSchema.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Decimal, Size = 10, DecimalPlaces = 5, AllowNulls = false });

            string actual = generator.Sql(definitionWithSchema);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableNullableColumn()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] Int NULL)", Constants.DEFAULT_SCHEMA); ;
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Int, AllowNulls = true });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithMultipleColumns()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] Int NULL,[c2] NVarChar NULL)", Constants.DEFAULT_SCHEMA); ;
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Int });
            definition.Columns.Add(new ColumnDefinition { Name = "c2", DataType = SqlDbType.NVarChar });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithStringLikeColumn()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] NVarChar(100) NULL)", Constants.DEFAULT_SCHEMA); ;
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.NVarChar, Size = 100 });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithStringLikeColumnNoSize()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] NVarChar NULL)", Constants.DEFAULT_SCHEMA); ;
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.NVarChar });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithStringLikeColumnMaxSize()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar(max) NULL)";
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.NVarChar, IsMaximumSize = true });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithBinaryColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Binary(10) NULL)";
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Binary, Size = 10 });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithDecimalColumnNoSize()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] Decimal NULL)", Constants.DEFAULT_SCHEMA); ;
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Decimal });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithIdentity()
        {
            string expected = string.Format("CREATE TABLE [{0}].[t1] ([c1] Int IDENTITY(8,1) NOT NULL)", Constants.DEFAULT_SCHEMA); ;
            definition.Columns.Add(new ColumnDefinition { Name = "c1", DataType = SqlDbType.Int, IdentitySeed = 8, AllowNulls = false});

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }
    }
}
