using System;
using CatalogMerger.Model;
using System.Collections.Generic;
using System.Linq;

namespace CatalogMerger.Service
{
    public class DataProcessor
    {
        public CompanyDataSource CompanyDataSourceInstance { get; set; } = new CompanyDataSource();
        public MergeCatalogDataSource MergeCatalogDataSourceInstance { get; set; } = new MergeCatalogDataSource();
        private string inputPath;
        private string outputPath;
        private string parentCompanyName;

        public bool ProcessData(string inputPathDirectory, string outputPathDirectory, string companyName)
        {
            inputPath = inputPathDirectory;
            outputPath = outputPathDirectory;
            parentCompanyName = companyName;
            var companyCollection = CompanyDataSourceInstance.ImportFromCsv(inputPath);
            return companyCollection.Count > 0 && MergeCatalogDataSourceInstance.ExportToCsv(companyName, outputPath, CompanyDataSourceInstance.CompanyCollection);
        }

        public void AddCatalogProduct(string sku, string description, string company)
        {
            var parentCompany = CompanyDataSourceInstance.CompanyCollection.Find(x => x.CompanyName.ToLower().Equals(company.ToLower()));
            var sameProductExist = parentCompany.CatalogDataSourceInstance.CatalogList.FindAll(x => x.SKU.Equals(sku) && x.Description.Equals(description));
            if (sameProductExist.Count.Equals(0))
            {
                Catalog catalogInstance = new Catalog { SKU = sku, Description = description }; parentCompany.CatalogDataSourceInstance.CatalogList.Add(catalogInstance);
                parentCompany.CatalogDataSourceInstance.ExportToCsv(inputPath);
                MergeCatalogDataSourceInstance.ExportToCsv(parentCompanyName, outputPath, CompanyDataSourceInstance.CompanyCollection);
            }
            else
            {
                Console.WriteLine("Already Catalog data exist");
            }
        }

        public void RemoveCatalogProduct(string sku, string company)
        {
            var parentCompany = CompanyDataSourceInstance.CompanyCollection.Find(x => x.CompanyName.ToLower().Equals(company.ToLower()));
            var barcodeList = parentCompany.BarcodeDataSourceInstance.BarcodeList.FindAll(x => x.SKU.Equals(sku));
            var numberOfSuppliersRemoved = 0;
            foreach (var barcode in barcodeList)
            {
                numberOfSuppliersRemoved += parentCompany.SuppliersDataSourceInstance.SupplierList.RemoveAll(x => x.ID.Equals(barcode.SupplierID));
            }
            var numberOfBarcodeRemoved = parentCompany.BarcodeDataSourceInstance.BarcodeList.RemoveAll(x => x.SKU.Equals(sku));
            var numberOfCatalogRemoved = parentCompany.CatalogDataSourceInstance.CatalogList.RemoveAll(x => x.SKU.Equals(sku));
            if (numberOfSuppliersRemoved > 0)
                parentCompany.SuppliersDataSourceInstance.ExportToCsv(inputPath);
            if (numberOfBarcodeRemoved > 0)
                parentCompany.BarcodeDataSourceInstance.ExportToCsv(inputPath);
            if (numberOfCatalogRemoved > 0)
                parentCompany.CatalogDataSourceInstance.ExportToCsv(inputPath);
            MergeCatalogDataSourceInstance.ExportToCsv(parentCompanyName, outputPath, CompanyDataSourceInstance.CompanyCollection);
        }

        public void AddSuppliers(string sku, string id, string name, string company, List<string> barcodeList)
        {
            var parentCompany = CompanyDataSourceInstance.CompanyCollection.Find(x => x.CompanyName.ToLower().Equals(company.ToLower()));
            var sameSupplierExist = parentCompany.SuppliersDataSourceInstance.SupplierList.FindAll(x => x.ID.Equals(id));
            if (sameSupplierExist.Count.Equals(0))
            {
                Suppliers suppliersInstance = new Suppliers { ID = id, Name = name };
                parentCompany.SuppliersDataSourceInstance.SupplierList.Add(suppliersInstance);
                parentCompany.SuppliersDataSourceInstance.ExportToCsv(inputPath);
            }
            else
            {
                Console.WriteLine("Already Supplier data exist");
            }

            bool isBarCodeUpdate = false;
            foreach (var barcodeData in barcodeList)
            {
                var sameBarcodeExist = parentCompany.BarcodeDataSourceInstance.BarcodeList.FindAll(x => x.SKU.Equals(sku) && x.SupplierID.Equals(id) && x.Barcode.Equals(barcodeData));
                if (sameBarcodeExist.Count.Equals(0))
                {
                    isBarCodeUpdate = true;
                    var barcodeInstance = new Barcodes { SKU = sku, SupplierID = id, Barcode = barcodeData };
                    parentCompany.BarcodeDataSourceInstance.BarcodeList.Add(barcodeInstance);
                }
            }
            if (isBarCodeUpdate)
                parentCompany.BarcodeDataSourceInstance.ExportToCsv(inputPath);
            MergeCatalogDataSourceInstance.ExportToCsv(parentCompanyName, outputPath, CompanyDataSourceInstance.CompanyCollection);
        }
    }
}
