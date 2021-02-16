using System;
using CatalogMerger.Service;
using CatalogMerger.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatalogMergerTest
{
    [TestClass]
    public class CatalogDataSourceTest
    {
        [TestMethod]
        public void Given_FilePath_Null_Then_ExportToCsv_Returns_False()
        {
            string filePath = null;
            CatalogDataSource catalogDataSource = new CatalogDataSource();
            bool isSuccess = catalogDataSource.ExportToCsv(filePath);
            Assert.IsFalse(isSuccess);
        }
    }
}
