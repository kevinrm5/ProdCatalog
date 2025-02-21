using System.Reflection;
using ProductModel;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            var existingCategory = await appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Code == category.Code);

            if (existingCategory == null)
            {
                var result = await appDbContext.Categories.AddAsync(category);
                await appDbContext.SaveChangesAsync();
                return result.Entity;
            }

            return existingCategory;
        }

        public async Task<Category> DeleteCategory(Guid Id)
        {
            var result = await appDbContext.Categories
                .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.Categories.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> GetCategory(Guid Id)
        {
            return await appDbContext.Categories
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await appDbContext.Categories.ToListAsync();
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            var result = await appDbContext.Categories
                .FirstOrDefaultAsync(e => e.Code == category.Code);

            if (result != null)
            {
                result.Name = category.Name;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task BulkInsertCategories(IEnumerable<Category> categories)
        {
            var categoryCodes = categories.Select(c => c.Code).ToList();
            var existingCategories = await appDbContext.Categories
                .Where(c => categoryCodes.Contains(c.Code))
                .Select(c => c.Code)
                .ToListAsync();

            var newCategories = categories
                .Where(c => !existingCategories.Contains(c.Code))
                .ToList();

            if (newCategories.Any())
            {
                await appDbContext.Categories.AddRangeAsync(newCategories);
                await appDbContext.SaveChangesAsync();
            }
        }

    }
}
