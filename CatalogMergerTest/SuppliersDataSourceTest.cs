using System;
using CatalogMerger.Service;
using CatalogMerger.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatalogMergerTest
{
    [TestClass]
    public class SuppliersDataSourceTest
    {
        [TestMethod]
        public void Given_FilePath_Null_Then_ExportToCsv_Returns_False()
        {
            string filePath = null;
            SuppliersDataSource suppliersDataSource = new SuppliersDataSource();
            bool isSuccess = suppliersDataSource.ExportToCsv(filePath);
            Assert.IsFalse(isSuccess);
        }
    }
}
