namespace IntegrationTestingLibraryForSqlServer
{
    public class ColumnDefinitionRaw
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int? Size { get; set; }
        public byte? DecimalPlaces { get; set; }
        public bool AllowNulls { get; set; }
        public int? IdentitySeed { get; set; }
    }
}
