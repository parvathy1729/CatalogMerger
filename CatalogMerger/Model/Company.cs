using CatalogMerger.Service;

namespace CatalogMerger.Model
{
    public class Company
    {
        public string CompanyName { get; set; }
        public CatalogDataSource CatalogDataSourceInstance { get; set; } = new CatalogDataSource();
        public SuppliersDataSource SuppliersDataSourceInstance { get; set; } = new SuppliersDataSource();
        public BarcodeDataSource BarcodeDataSourceInstance { get; set; } = new BarcodeDataSource();
    }
}
