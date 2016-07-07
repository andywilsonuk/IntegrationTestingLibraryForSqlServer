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
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            if (!definition.HasBody) throw new ArgumentException("Body text is required", nameof(definition));
            return string.Format(CreateProcedureFormat, definition.Name.Qualified, CreateCommaSeparatedParameters(definition), definition.Body);
        }

        private string CreateCommaSeparatedParameters(ProcedureDefinition definition)
        {
            return string.Join(",", definition.ParametersWithoutReturnValue.Select(x => GetFormattedParameterLine(x)));
        }

        private string GetFormattedParameterLine(ProcedureParameter parameter)
        {
            return string.Format(
                "{0} {1}{2}",
                parameter.QualifiedName,
                GetFormattedDataType(parameter),
                GetFormattedDirection(parameter));
        }

        private string GetFormattedDataType(ProcedureParameter parameter)
        {
            var sizeColumn = parameter as VariableSizeProcedureParameter;
            if (sizeColumn != null)
            {
                string size = sizeColumn.IsMaximumSize ? "max" : sizeColumn.Size.ToString();
                return string.Format("{0}({1})", parameter.DataType, size);
            }
            var decimalColumn = parameter as DecimalProcedureParameter;
            if (decimalColumn != null)
            {
                return string.Format("{0}({1},{2})", parameter.DataType, decimalColumn.Precision, decimalColumn.Scale);
            }
            return parameter.DataType.ToString();
        }

        private string GetFormattedDirection(ProcedureParameter parameter)
        {
            if (parameter.Direction == ParameterDirection.Input) return null;
            return " OUTPUT";
        }

        private const string CreateProcedureFormat = "create procedure {0} {1} as begin {2} end";
    }
}
