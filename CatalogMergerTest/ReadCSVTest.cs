using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogMerger.Util;

namespace CatalogMergerTest
{
    [TestClass]
    public class ReadCSVTest
    {
        [TestMethod]
        public void Given_FilePath_Null_Then_ConvertCsvToDataTable_Returns_Empty_DataTable()
        {
            string filePath = null;
            DataTable dataTable = ReadCsv.ConvertCsvToDataTable(filePath);
            Assert.IsTrue(dataTable.Rows.Count == 0);
        }
    }
}
