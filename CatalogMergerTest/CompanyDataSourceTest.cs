using System;
using System.Collections.Generic;
using CatalogMerger.Model;
using CatalogMerger.Service;
using CatalogMerger.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatalogMergerTest
{
    [TestClass]
    public class CompanyDataSourceTest
    {
        [TestMethod]
        public void Given_FilePath_Null_Then_Returns_Empty_CompanyList()
        {
            string filePath = null;
            CompanyDataSource companyDataSource = new CompanyDataSource();
            List<Company> companyList = companyDataSource.ImportFromCsv(filePath);
            Assert.AreEqual(companyList.Count, 0);
        }
    }
}
