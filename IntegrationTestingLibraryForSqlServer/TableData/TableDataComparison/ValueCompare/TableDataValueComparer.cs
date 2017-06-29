namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataValueComparer
    {
        bool IsMatch(object x, object y);
    }
}
