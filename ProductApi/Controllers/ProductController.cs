using System.Reflection;
using ProductApi.Models;
using ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                var products = await productRepository.GetProducts();

                if (products == null || !products.Any())
                {
                    return NotFound("No products found.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving products.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(string productCode)
        {
            try
            {
                var result = await productRepository.GetProduct(productCode);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving Product.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                var pro = await productRepository.GetProduct(product.Code);
                if (pro != null)
                {
                    ModelState.AddModelError("product", "Product code already in use");
                    return BadRequest(ModelState);
                }
                var createdProduct = await productRepository.AddProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { code = createdProduct.Code }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving products.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(string code, Product product)
        {
            try
            {
                if (code != product.Code)
                {
                    return BadRequest("Product ID mismatch");
                }
                var productToUpdate = await productRepository.GetProduct(code);
                if (productToUpdate == null)
                {
                    return NotFound($"Product with Code = {code} not found");
                }
                return await productRepository.UpdateProduct(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating product.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(string code)
        {
            try
            {
                var productToDelete = await productRepository.GetProduct(code);
                if (productToDelete == null)
                {
                    return NotFound($"Product with code {code} not found");
                }
                return await productRepository.DeleteProduct(code);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting data");
            }
        }

    }
}
