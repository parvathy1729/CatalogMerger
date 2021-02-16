using System.Collections.Generic;
using CatalogMerger.Interface;
using CatalogMerger.Model;
using CatalogMerger.Util;

namespace CatalogMerger.Service
{
    public class BarcodeDataSource : IDataSourceExport<Barcodes>
    {
        public string FileName { get; set; }
        public List<Barcodes> BarcodeList { get; set; } = new List<Barcodes>();

        public bool ExportToCsv(string outputPath)
        {
            return !string.IsNullOrWhiteSpace(outputPath) && BarcodeList.SaveCsvFile(outputPath, FileName);
        }
    }
}
