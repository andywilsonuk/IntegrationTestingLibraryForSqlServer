namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataValueComparerPipeline : TableDataValueComparer
    {
        private static readonly TableDataValueComparerPipeElement[] elements = 
        {
            new TableDataNullValueComparer(),
            new TableDataNumberValueComparer(),
            new TableDataBooleanValueComparer(),
            new TableDataGuidValueComparer(),
            new TableDataDateTimeValueComparer(),
            new TableDataCaseSensitiveStringValuePipeElementComparer(),
            new TableDataDefaultValueComparer()
        };

        public bool IsMatch(object x, object y)
        {
            var args = new TableDataValueComparerPipeElementArguments { X = x, Y = y };
            foreach (var element in elements)
            {
                element.Process(args);
            }

            return args.MatchStatus == MatchedValueComparer.IsMatch;
        }
    }
}
