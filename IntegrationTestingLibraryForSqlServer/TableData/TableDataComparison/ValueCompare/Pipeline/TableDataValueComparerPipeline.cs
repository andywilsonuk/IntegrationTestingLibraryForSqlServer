using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataValueComparerPipeline : TableDataValueComparer
    {
        private static TableDataValueComparerPipeElement[] elements = new TableDataValueComparerPipeElement[]
        {
            new TableDataNullValueComparer(),
            new TableDataGuidValueComparer(),
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
