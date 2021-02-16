using System.Collections.Generic;
using System.Linq;
using CatalogMerger.Interface;
using CatalogMerger.Model;
using CatalogMerger.Util;

namespace CatalogMerger.Service
{
    public class MergeCatalogDataSource : IDataSourceExport<MergeCatalog>
    {
        public List<MergeCatalog> MergeCatalogCompanyCollection { get; set; } = new List<MergeCatalog>();

        public bool ExportToCsv(string outputPath)
        {
            return !string.IsNullOrWhiteSpace(outputPath) && MergeCatalogCompanyCollection.SaveCsvFile(outputPath, "result_output.csv");
        }
        public bool ExportToCsv(string parentCompanyName, string outputDirectory, List<Company> companies)
        {
            var parentCompany = companies.Find(x => x.CompanyName.ToLower().Equals(parentCompanyName.ToLower()));
            MergeCatalogCompanyCollection = new List<MergeCatalog>();
            foreach (var mergeCatalog in parentCompany.CatalogDataSourceInstance.CatalogList.Select(catalog => new MergeCatalog
            {
                Company = parentCompanyName,
                Description = catalog.Description,
                SKU = catalog.SKU
            }))
            {
                MergeCatalogCompanyCollection.Add(mergeCatalog);
            }

            var companyList = companies.FindAll(x => !x.CompanyName.ToLower().Equals(parentCompanyName.ToLower()));
            foreach (var company in companyList)
            {
                foreach (var catalog in company.CatalogDataSourceInstance.CatalogList)
                {
                    var barcodeList = company.BarcodeDataSourceInstance.BarcodeList.FindAll(x => x.SKU.Equals(catalog.SKU));
                    bool isAdd = false;
                    foreach (var barcode in barcodeList)
                    {
                        var sameParentBarcodeList = parentCompany.BarcodeDataSourceInstance.BarcodeList.FindAll(x => x.SupplierID.Equals(barcode.SupplierID));
                        if (sameParentBarcodeList.Count.Equals(0))
                        {
                            isAdd = true;
                            break;
                        }

                        var sameBarcodeExistInParent = sameParentBarcodeList.Find(x => x.Barcode.Equals(barcode.Barcode));
                        if (sameBarcodeExistInParent != null) continue;
                        isAdd = true;
                        break;
                    }

                    if (!isAdd) continue;
                    var mergeCatalog = new MergeCatalog
                    {
                        Company = company.CompanyName,
                        Description = catalog.Description,
                        SKU = catalog.SKU
                    };
                    MergeCatalogCompanyCollection.Add(mergeCatalog);
                }
            }

            return ExportToCsv(outputDirectory);
        }
    }
}
