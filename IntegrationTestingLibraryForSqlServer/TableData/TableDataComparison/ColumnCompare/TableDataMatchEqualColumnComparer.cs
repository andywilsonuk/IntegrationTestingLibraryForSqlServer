namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchEqualColumnComparer : TableDataMatchSubsetColumnComparer
    {
        public override bool IsMatch()
        {
            if (columnsX.Count != columnsY.Count) return false;
            return base.IsMatch();
        }
    }
}
