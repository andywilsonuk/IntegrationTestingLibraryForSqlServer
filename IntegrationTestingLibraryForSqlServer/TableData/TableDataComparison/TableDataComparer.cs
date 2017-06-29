namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataComparer
    {
        bool IsMatch(TableData x, TableData y);
    }
}
