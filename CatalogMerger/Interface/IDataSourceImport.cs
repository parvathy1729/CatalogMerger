using System.Collections.Generic;

namespace CatalogMerger.Interface
{
    public interface IDataSourceImport<T>
    {
        List<T> ImportFromCsv(string inputPath);
    }
}
