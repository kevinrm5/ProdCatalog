using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ProductModel;

namespace ProductTestCases.Tests
{
    public class ApiIntegrationTests
    {
        [Fact]
        public async Task SendDataToApi_ShouldReturnSuccess_WhenValidProductsAreSent()
        {
            var products = new List<ProductCategoryDto>
        {
            new ProductCategoryDto { ProductName = "Laptop", ProductCode = "D001", CategoryName = "Electronics", CategoryCode = "C001" }
        };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/");

                var response = await client.PostAsJsonAsync("api/products-bulk", products);

                Assert.True(response.IsSuccessStatusCode);
            }
        }
    }
}
