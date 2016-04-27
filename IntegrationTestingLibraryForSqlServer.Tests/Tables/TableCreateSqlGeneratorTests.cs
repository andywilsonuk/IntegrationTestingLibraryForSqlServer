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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableNullThrowsException()
        {
            generator.Sql(null);
        }

        [TestMethod]
        public void CreateTableNullableColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Int NULL)";
            definition.Columns.Add(new ColumnDefinition("c1", SqlDbType.Int) { AllowNulls = true });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithMultipleColumns()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Int NULL,[c2] NVarChar NULL)";
            definition.Columns.Add(new ColumnDefinition("c1", SqlDbType.Int));
            definition.Columns.Add(new ColumnDefinition("c2",SqlDbType.NVarChar));

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithStringLikeColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar(100) NULL)";
            definition.Columns.Add(new SizeableColumnDefinition("c1", SqlDbType.NVarChar) { Size = 100 });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithStringLikeColumnNoSize()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar NULL)";
            definition.Columns.Add(new ColumnDefinition("c1", SqlDbType.NVarChar));

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithStringLikeColumnMaxSize()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar(max) NULL)";
            definition.Columns.Add(new SizeableColumnDefinition("c1", SqlDbType.NVarChar) { IsMaximumSize = true });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithBinaryColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Binary(10) NULL)";
            definition.Columns.Add(new SizeableColumnDefinition("c1", SqlDbType.Binary) { Size = 10 });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithDecimalColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Decimal(10,5) NULL)";
            definition.Columns.Add(new DecimalColumnDefinition("c1") { Precision = 10, Scale = 5 });

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateTableWithIdentity()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Int IDENTITY(8,1) NOT NULL)";
            definition.Columns.Add(new IntegerColumnDefinition("c1", SqlDbType.Int) { IdentitySeed = 8, AllowNulls = false});

            string actual = generator.Sql(definition);

            Assert.AreEqual(expected, actual);
        }
    }
}
