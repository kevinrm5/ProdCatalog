using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/products-bulk")]
    [ApiController]
    public class ProductBulkController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductBulkController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> BulkInsert([FromBody] List<ProductCategoryDto> requestData)
        {
            if (requestData == null || !requestData.Any())
                return BadRequest("Invalid input data");

            var products = new List<Product>();
            var categories = new List<Category>();

            foreach (var row in requestData)
            {
                var product = new Product
                {
                    Code = row.ProductCode,
                    Name = row.ProductName,
                    CategoryCode = row.CategoryCode
                };

                var category = new Category
                {
                    Code = row.CategoryCode,
                    Name = row.CategoryName
                };

                if (!products.Any(p => p.Code == product.Code))
                    products.Add(product);

                if (!categories.Any(c => c.Code == category.Code))
                    categories.Add(category);
            }

            if (products.Any())
                await _productRepository.BulkInsertProducts(products);

            if (categories.Any())
                await _categoryRepository.BulkInsertCategories(categories);

            return Ok(new { Message = "Bulk insert operation completed" });
        }
    }
}
