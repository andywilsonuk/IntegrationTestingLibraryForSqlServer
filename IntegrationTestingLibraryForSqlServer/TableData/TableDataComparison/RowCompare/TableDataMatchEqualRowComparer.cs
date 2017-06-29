namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchEqualRowComparer : TableDataMatchSubsetRowComparer
    {
        public override bool IsMatch()
        { 
            if (x.Count != y.Count) return false;
            return base.IsMatch();
        }
    }
}
