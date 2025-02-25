using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using System.Collections.Generic;
using System.IO;
using ProductModel;
using ProductConsoleApp;

namespace ProductTestCases.Tests
{
    public class CsvReaderTests
    {
        [Fact]
        public void ReadCsv_ShouldReturnValidProducts_WhenCsvIsValid()
        {
            string projectRoot = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(projectRoot, "TestExampleFile.csv");
            string testCsv = "ProductName,ProductCode,CategoryName,CategoryCode\nLaptop,D001,Electronics,C001";
            File.WriteAllText(filePath, testCsv);

            List<ProductCategoryDto> products = ProductConsoleApp.Program.ReadCsv(filePath);

            Assert.Single(products);
            Assert.Equal("Laptop", products[0].ProductName);
            Assert.Equal("D001", products[0].ProductCode);

            File.Delete("TestExampleFile.csv");
        }
    }
}
