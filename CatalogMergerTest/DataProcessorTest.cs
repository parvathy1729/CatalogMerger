using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogMerger.Service;

namespace CatalogMergerTest
{
    [TestClass]
    public class DataProcessorTest
    {
        [TestMethod]
        public void Given_InputDirectory_Null_Then_ProcessData_Returns_False()
        {
            DataProcessor dataProcessor = new DataProcessor();
            bool bReturn = dataProcessor.ProcessData(null, null, string.Empty);

            Assert.IsFalse(bReturn);
        }
    }
}
