using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableCreateSqlGeneratorTests
    {
        TableCreateSqlGenerator generator = new TableCreateSqlGenerator();
        TableDefinition definition = new TableDefinition("t1");

        [Fact]
        public void CreateTableNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => generator.Sql(null));
        }

        [Fact]
        public void CreateTableZeroColumnsThrowsException()
        {
            Assert.Throws<ArgumentException>(() => generator.Sql(definition));
        }

        [Fact]
        public void CreateTableNullableColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Int NULL)";
            definition.Columns.AddInteger("c1", SqlDbType.Int).AllowNulls = true;

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithMultipleColumns()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Int NULL,[c2] NVarChar(1) NULL)";
            definition.Columns.AddInteger("c1", SqlDbType.Int);
            definition.Columns.AddString("c2", SqlDbType.NVarChar);

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithStringLikeColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar(100) NULL)";
            definition.Columns.AddString("c1", SqlDbType.NVarChar).Size = 100;

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithStringLikeColumnNoSize()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar(1) NULL)";
            definition.Columns.AddString("c1", SqlDbType.NVarChar);

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithStringLikeColumnMaxSize()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] NVarChar(max) NULL)";
            definition.Columns.AddString("c1", SqlDbType.NVarChar).IsMaximumSize = true;

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithBinaryColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Binary(10) NULL)";
            definition.Columns.AddBinary("c1", SqlDbType.Binary).Size = 10;

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithDecimalColumn()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Decimal(10,5) NULL)";
            var column = definition.Columns.AddDecimal("c1");
            column.Precision = 10;
            column.Scale = 5;

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTableWithIdentity()
        {
            string expected = "CREATE TABLE [dbo].[t1] ([c1] Int IDENTITY(8,1) NOT NULL)";
            var column = definition.Columns.AddInteger("c1", SqlDbType.Int);
            column.IdentitySeed = 8;
            column.AllowNulls = false;

            string actual = generator.Sql(definition);

            Assert.Equal(expected, actual);
        }
    }
}
