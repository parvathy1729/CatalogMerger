namespace CatalogMerger.Interface
{
    public interface IDataSourceExport<T>
    {
        bool ExportToCsv(string outputPath);
    }
}
