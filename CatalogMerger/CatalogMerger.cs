using System;
using System.Linq;
using System.Runtime.CompilerServices;
using CatalogMerger.Service;

namespace CatalogMerger
{
    class CatalogMerger
    {
        static void Main()
        {
            CatalogMerger catalogMerger = new CatalogMerger();
            catalogMerger.Process();
        }

        public void Process()
        {
            DataProcessor instanceDataProcessor = new DataProcessor();
            var isProcessed = ProcessCsvFile(instanceDataProcessor);

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine();
            if (isProcessed)
            {
                while (true)
                {
                    Console.WriteLine("BAU Operations - Please select any of following options");
                    Console.WriteLine("Add new product for catalog A: 1");
                    Console.WriteLine("Remove a product for catalog A: 2");
                    Console.WriteLine("Add a supplier for catalog B: 3");
                    Console.WriteLine("Enter 'E' for exit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine();
                    var input = Console.ReadLine();
                    if (input != null && input.ToLower().Equals("e"))
                        Environment.Exit(0);

                    if (int.TryParse(input, out var option))
                    {
                        switch (option)
                        {
                            case 1:
                                // Add product - Catalog A
                                AddProductToCatalogA(instanceDataProcessor);
                                break;

                            case 2:
                                // Remove product - Catalog A
                                RemoveProductFromCatalogA(instanceDataProcessor);
                                break;

                            case 3:
                                // Add new supplier - Catalog B
                                AddSupplierToCatalogB(instanceDataProcessor);
                                break;

                            default:
                                Console.WriteLine("Invalid Option - Please enter a valid option");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Option - Please enter a valid option");
                    }
                }
            }
        }

        private void AddProductToCatalogA(DataProcessor instanceDataProcessor)
        {
            Console.WriteLine("Enter product SKU: ");
            string skuAdd = Console.ReadLine();
            Console.WriteLine("Enter product Description: ");
            string descriptionAdd = Console.ReadLine();
            instanceDataProcessor.AddCatalogProduct(skuAdd, descriptionAdd, "A");
        }

        private void RemoveProductFromCatalogA(DataProcessor instanceDataProcessor)
        {
            Console.WriteLine("Enter product SKU: ");
            string skuRemove = Console.ReadLine();
            instanceDataProcessor.RemoveCatalogProduct(skuRemove, "A");
        }

        private void AddSupplierToCatalogB(DataProcessor instanceDataProcessor)
        {
            Console.WriteLine("Enter product SKU: ");
            string skuAddSupplier = Console.ReadLine();
            Console.WriteLine("Enter supplierId: ");
            string supplierId = Console.ReadLine();
            Console.WriteLine("Enter supplierName: ");
            string supplierName = Console.ReadLine();
            Console.WriteLine("Enter barcodes separated by comma: ");
            string barCodes = Console.ReadLine();
            if (barCodes != null)
            {
                string[] barcodeArray = barCodes.Split(',');
                instanceDataProcessor.AddSuppliers(skuAddSupplier, supplierId, supplierName, "B", barcodeArray.ToList());
            }
        }

        private bool ProcessCsvFile(DataProcessor instanceDataProcessor)
        {
            Console.WriteLine("Enter the input directory: ");
            var inputDirectory = Console.ReadLine();

            Console.WriteLine("Enter the output directory: ");
            var outputDirectory = Console.ReadLine();

            bool isProcessed = instanceDataProcessor.ProcessData(inputDirectory, outputDirectory, "A");
            Console.WriteLine(isProcessed ? "Please see inside the output directory for result csv File " : "Could not process the data.... ");
            return isProcessed;
        }

    }
}
