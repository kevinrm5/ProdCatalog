using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductModel;

namespace ProductTestCases.Tests
{
    public class DataValidationTests
    {
        [Fact]
        public void Product_ShouldBeValid_WhenAllFieldsAreFilled()
        {
            var product = new ProductCategoryDto
            {
                ProductName = "Laptop",
                ProductCode = "D001",
                CategoryName = "Electronics",
                CategoryCode = "C001"
            };

            bool isValid = product != null && !string.IsNullOrEmpty(product.ProductName) && !string.IsNullOrEmpty(product.ProductCode);

            Assert.True(isValid);
        }

        [Fact]
        public void Product_ShouldBeInvalid_WhenProductNameIsMissing()
        {
            var product = new ProductCategoryDto
            {
                ProductName = "",
                ProductCode = "D001",
                CategoryName = "Electronics",
                CategoryCode = "C001"
            };

            bool isValid = !string.IsNullOrEmpty(product.ProductName);

            Assert.False(isValid);
        }
    }
}
