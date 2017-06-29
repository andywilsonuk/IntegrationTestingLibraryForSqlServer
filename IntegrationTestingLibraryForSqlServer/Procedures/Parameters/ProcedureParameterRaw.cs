using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureParameterRaw
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int? Size { get; set; }
        public byte? DecimalPlaces { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
