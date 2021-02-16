using System.Collections.Generic;
using CatalogMerger.Interface;
using CatalogMerger.Model;
using CatalogMerger.Util;

namespace CatalogMerger.Service
{
    public class CatalogDataSource : IDataSourceExport<Catalog>
    {
        public string FileName { get; set; }
        public List<Catalog> CatalogList { get; set; } = new List<Catalog>();

        public bool ExportToCsv(string outputPath)
        {
            return !string.IsNullOrWhiteSpace(outputPath) && CatalogList.SaveCsvFile(outputPath, FileName);
        }
    }
}
