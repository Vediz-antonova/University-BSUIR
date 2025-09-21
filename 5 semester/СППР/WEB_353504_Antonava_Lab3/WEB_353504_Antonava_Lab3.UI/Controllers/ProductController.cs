using Microsoft.AspNetCore.Mvc;

namespace WEB_353504_Antonava_Lab3.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var categoriesResponse = await _categoryService.GetCategoryListAsync();
            if (!categoriesResponse.Successfull)
                return NotFound(categoriesResponse.ErrorMessage);

            var currentCategoryName = category == null
                ? "Все категории"
                : categoriesResponse.Data.FirstOrDefault(c => c.NormalizedName == category)?.Name ?? "Неизвестно";

            ViewData["currentCategory"] = currentCategoryName;
            ViewData["categories"] = categoriesResponse.Data;

            var productResponse = await _productService.GetProductListAsync(category, pageNo);
            if (!productResponse.Successfull)
                return NotFound(productResponse.ErrorMessage);

            return View(productResponse.Data);
        }
    }
}
