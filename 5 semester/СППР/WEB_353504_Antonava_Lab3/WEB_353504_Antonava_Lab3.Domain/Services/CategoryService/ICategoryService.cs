using WEB_353504_Antonava_Lab3.Domain.Entities;
using WEB_353504_Antonava_Lab3.Domain.Models;

namespace WEB_353504_Antonava_Lab3.Domain.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
