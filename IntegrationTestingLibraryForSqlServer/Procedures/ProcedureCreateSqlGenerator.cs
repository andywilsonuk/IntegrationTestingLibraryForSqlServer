using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureCreateSqlGenerator
    {
        public string Sql(ProcedureDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            definition.EnsureValid(true);
            return string.Format(CreateProcedureFormat, definition.Name, this.CreateCommaSeparatedParameters(definition), definition.Body);
        }

        private string CreateCommaSeparatedParameters(ProcedureDefinition definition)
        {
            return string.Join(",", definition.ParametersWithoutReturnValue.Select(x => this.GetFormattedParameterLine(x)));
        }

        private string GetFormattedParameterLine(ProcedureParameter parameter)
        {
            return string.Format(
                "{0} {1}{2}",
                parameter.QualifiedName,
                this.GetFormattedDataType(parameter),
                this.GetFormattedDirection(parameter));
        }

        private string GetFormattedDataType(ProcedureParameter parameter)
        {
            switch (parameter.DataType)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                    if (!parameter.Size.HasValue) break;
                    return string.Format("{0}({1})", parameter.DataType, parameter.Size.Value);
                case SqlDbType.Decimal:
                    if (!parameter.Size.HasValue && !parameter.DecimalPlaces.HasValue) break;
                    var parameterSize = parameter.Size ?? 0;
                    var parameterDecimalPlaces = parameter.DecimalPlaces ?? 0;
                    return string.Format("{0}({1},{2})", parameter.DataType, parameterSize, parameterDecimalPlaces);
            }
            return string.Format("{0}", parameter.DataType);
        }

        private string GetFormattedDirection(ProcedureParameter parameter)
        {
            if (parameter.Direction == ParameterDirection.Input) return null;
            return " OUTPUT";
        }

        private const string CreateProcedureFormat = "create procedure [{0}] {1} as begin {2} end";
    }
}
