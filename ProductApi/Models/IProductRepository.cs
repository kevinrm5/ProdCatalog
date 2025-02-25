using System.Reflection;
using ProductModel;

namespace ProductApi.Models
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product> Items, int TotalCount)> GetProducts(int page, int pageSize, string? productCode);
        Task<Product> GetProduct(string productCode);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(string productCode);
        Task BulkInsertProducts(IEnumerable<Product> products);
    }
}
