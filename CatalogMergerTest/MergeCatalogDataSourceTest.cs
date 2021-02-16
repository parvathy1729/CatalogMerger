using System;
using CatalogMerger.Service;
using CatalogMerger.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatalogMergerTest
{
    [TestClass]
    public class MergeCatalogDataSourceTest
    {
        [TestMethod]
        public void Given_FilePath_Null_Then_ExportToCsv_Returns_False()
        {
            string filePath = null;
            MergeCatalogDataSource mergeCatalogDataSource = new MergeCatalogDataSource();
            bool isSuccess = mergeCatalogDataSource.ExportToCsv(filePath);
            Assert.IsFalse(isSuccess);
        }
    }
}
