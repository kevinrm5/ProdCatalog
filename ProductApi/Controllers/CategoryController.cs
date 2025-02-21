using System.Reflection;
using ProductApi.Models;
using ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var categories = await categoryRepository.GetCategories();

                if (categories == null || !categories.Any())
                {
                    return NotFound("No categories found.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving categories.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetCategory(Guid Id)
        {
            try
            {
                var result = await categoryRepository.GetCategory(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving Category.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCatgory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                var emp = await categoryRepository.GetCategory(category.Id);
                if (emp != null)
                {
                    ModelState.AddModelError("category", "Category code already in use");
                    return BadRequest(ModelState);
                }
                var createdCategory = await categoryRepository.AddCategory(category);
                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving categories.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Category>> UpdateCategory(Guid Id, Category category)
        {
            try
            {
                if (Id != category.Id)
                {
                    return BadRequest("Category ID mismatch");
                }
                var categoryToUpdate = await categoryRepository.GetCategory(Id);
                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with Code = {Id} not found");
                }
                return await categoryRepository.UpdateCategory(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating category.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid Id)
        {
            try
            {
                var categoryToDelete = await categoryRepository.GetCategory(Id);
                if (categoryToDelete == null)
                {
                    return NotFound($"Category with code {Id} not found");
                }
                return await categoryRepository.DeleteCategory(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting data");
            }
        }

    }
}
