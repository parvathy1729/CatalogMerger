using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CatalogMerger.Interface;
using CatalogMerger.Model;
using CatalogMerger.Util;

namespace CatalogMerger.Service
{
    public class CompanyDataSource : IDataSourceImport<Company>
    {
        public List<Company> CompanyCollection { get; set; } = new List<Company>();

        public List<Company> ImportFromCsv(string inputPath)
        {
            CompanyCollection = new List<Company>();
            if (inputPath != null)
            {
                var csvFiles = new DirectoryInfo(inputPath).GetFiles("*.csv").ToList();
                foreach (var csvFile in csvFiles)
                {
                    CatalogDataSource catalogDataSourceInstance = new CatalogDataSource();
                    BarcodeDataSource barcodeDataSourceInstance = new BarcodeDataSource();
                    SuppliersDataSource suppliersDataSourceInstance = new SuppliersDataSource();
                    string companyName;
                    var table = ReadCsv.ConvertCsvToDataTable(csvFile.FullName);

                    var name = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

                    if (csvFile.Name.ToLower().Contains(DataComponents.Barcodes))
                    {
                        companyName = csvFile.Name.Substring(DataComponents.Barcodes.Length,
                            csvFile.Name.Length - (DataComponents.Barcodes.Length + 4));
                        barcodeDataSourceInstance.BarcodeList = ReadCsv.ConvertToList<Barcodes>(table);
                        barcodeDataSourceInstance.FileName = csvFile.Name;
                    }
                    else if (csvFile.Name.ToLower().Contains(DataComponents.Catalog))
                    {
                        companyName = csvFile.Name.Substring(DataComponents.Catalog.Length,
                            csvFile.Name.Length - (DataComponents.Catalog.Length + 4));
                        catalogDataSourceInstance.CatalogList = ReadCsv.ConvertToList<Catalog>(table).Distinct(new CatalogCompare()).ToList();
                        catalogDataSourceInstance.FileName = csvFile.Name;
                    }
                    else
                    {
                        companyName = csvFile.Name.Substring(DataComponents.Suppliers.Length,
                            csvFile.Name.Length - (DataComponents.Suppliers.Length + 4));
                        suppliersDataSourceInstance.SupplierList = ReadCsv.ConvertToList<Suppliers>(table);
                        suppliersDataSourceInstance.FileName = csvFile.Name;
                    }

                    var company = CompanyCollection.Find(x => x.CompanyName.Equals(companyName));
                    if (company != null)
                    {
                        if (barcodeDataSourceInstance.BarcodeList.Count > 0)
                        {
                            company.BarcodeDataSourceInstance = barcodeDataSourceInstance;
                        }

                        if (suppliersDataSourceInstance.SupplierList.Count > 0)
                        {
                            company.SuppliersDataSourceInstance = suppliersDataSourceInstance;
                        }

                        if (catalogDataSourceInstance.CatalogList.Count > 0)
                        {
                            company.CatalogDataSourceInstance = catalogDataSourceInstance;
                        }
                    }
                    else
                    {
                        var newCompany = new Company
                        {
                            CompanyName = companyName,
                            CatalogDataSourceInstance = catalogDataSourceInstance,
                            BarcodeDataSourceInstance = barcodeDataSourceInstance,
                            SuppliersDataSourceInstance = suppliersDataSourceInstance
                        };

                        CompanyCollection.Add(newCompany);
                    }
                }
            }

            return CompanyCollection;
        }
    }
}
