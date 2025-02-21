using System.Reflection;
using ProductModel;

namespace ProductApi.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(Guid categoryCode);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(Guid categoryCode);
        Task BulkInsertCategories(IEnumerable<Category> categories);
    }
}
