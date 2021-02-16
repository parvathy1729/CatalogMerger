using System;
using CatalogMerger.Service;
using CatalogMerger.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatalogMergerTest
{
    [TestClass]
    public class BarcodeDataSourceTest
    {
        [TestMethod]
        public void Given_FilePath_Null_Then_ExportToCsv_Returns_False()
        {
            string filePath = null;
            BarcodeDataSource barcodeDataSource = new BarcodeDataSource();
            bool isSuccess = barcodeDataSource.ExportToCsv(filePath);
            Assert.IsFalse(isSuccess);
        }
    }
}
