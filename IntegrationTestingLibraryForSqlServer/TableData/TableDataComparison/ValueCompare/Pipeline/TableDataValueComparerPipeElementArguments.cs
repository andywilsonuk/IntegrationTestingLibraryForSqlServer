using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataValueComparerPipeElementArguments
    {
        public TableDataValueComparerPipeElementArguments()
        {
            this.MatchStatus = MatchedValueComparer.NotYetCompared;
        }

        public object X { get; set; }
        public object Y { get; set; }
        public MatchedValueComparer MatchStatus { get; set; }

        public bool IsCompared
        {
            get { return this.MatchStatus != MatchedValueComparer.NotYetCompared; }
        }
    }

    public enum MatchedValueComparer
    {
        NotYetCompared,
        NoMatch,
        IsMatch
    }
}
