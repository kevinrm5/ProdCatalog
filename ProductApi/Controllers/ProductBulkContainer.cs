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

            var duplicateProductCodes = requestData
                .GroupBy(p => p.ProductCode)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            var duplicateCategoryCodes = requestData
                .GroupBy(c => c.CategoryCode)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateProductCodes.Any() || duplicateCategoryCodes.Any())
            {
                return BadRequest(new
                {
                    Message = "Duplicate codes found",
                    DuplicateProductCodes = duplicateProductCodes,
                    DuplicateCategoryCodes = duplicateCategoryCodes
                });
            }

            var products = requestData
                .Select(row => new Product
                {
                    Code = row.ProductCode,
                    Name = row.ProductName,
                    CategoryCode = row.CategoryCode
                })
                .Distinct()
                .ToList();

            var categories = requestData
                .Select(row => new Category
                {
                    Code = row.CategoryCode,
                    Name = row.CategoryName
                })
                .Distinct()
                .ToList();

            await _productRepository.BulkInsertProducts(products);
            await _categoryRepository.BulkInsertCategories(categories);

            return Ok(new { Message = "Bulk insert operation completed successfully" });
        }
    }
}
