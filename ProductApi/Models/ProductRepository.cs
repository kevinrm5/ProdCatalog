using System.Reflection;
using ProductModel;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var existingProduct = await appDbContext.Products
                .FirstOrDefaultAsync(p => p.Code == product.Code);

            if (existingProduct == null)
            {
                var result = await appDbContext.Products.AddAsync(product);
                await appDbContext.SaveChangesAsync();
                return result.Entity;
            }

            return existingProduct;
        }

        public async Task<Product> DeleteProduct(string productCode)
        {
            var result = await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Code == productCode);

            if (result != null)
            {
                appDbContext.Products.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Product> GetProduct(string productCode)
        {
            return await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Code == productCode);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await appDbContext.Products.ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            var result = await appDbContext.Products
                .FirstOrDefaultAsync(e => e.Code == product.Code);

            if (result != null)
            {
                result.Name = product.Name;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task BulkInsertProducts(IEnumerable<Product> products)
        {
            var productCodes = products.Select(p => p.Code).ToList();
            var existingProducts = await appDbContext.Products
                .Where(p => productCodes.Contains(p.Code))
                .Select(p => p.Code)
                .ToListAsync();

            var newProducts = products
                .Where(p => !existingProducts.Contains(p.Code))
                .ToList();

            if (newProducts.Any())
            {
                await appDbContext.Products.AddRangeAsync(newProducts);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
