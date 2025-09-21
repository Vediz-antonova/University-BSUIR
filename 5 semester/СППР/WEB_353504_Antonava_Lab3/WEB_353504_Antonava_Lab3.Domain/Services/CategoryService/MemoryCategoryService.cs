using WEB_353504_Antonava_Lab3.Domain.Entities;
using WEB_353504_Antonava_Lab3.Domain.Models;

namespace WEB_353504_Antonava_Lab3.Domain.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Корма", NormalizedName = "food" },
                new Category { Id = 2, Name = "Игрушки", NormalizedName = "toys" },
                new Category { Id = 3, Name = "Аксессуары", NormalizedName = "accessories" },
                new Category { Id = 4, Name = "Средства ухода", NormalizedName = "care-products" },
                new Category { Id = 5, Name = "Лакомства", NormalizedName = "treats" }
            };

            return Task.FromResult(ResponseData<List<Category>>.Success(categories));
        }
    }
}
