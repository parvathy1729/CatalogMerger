using System.Collections.Generic;
using CatalogMerger.Interface;
using CatalogMerger.Model;
using CatalogMerger.Util;

namespace CatalogMerger.Service
{
    public class SuppliersDataSource : IDataSourceExport<Suppliers>
    {
        public string FileName { get; set; }
        public List<Suppliers> SupplierList { get; set; } = new List<Suppliers>();

        public bool ExportToCsv(string outputPath)
        {
            return !string.IsNullOrWhiteSpace(outputPath) && SupplierList.SaveCsvFile(outputPath, FileName);
        }
    }
}
