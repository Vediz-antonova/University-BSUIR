using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WEB_353504_Antonava_Lab3.Domain.Entities;
using WEB_353504_Antonava_Lab3.Domain.Models;
using WEB_353504_Antonava_Lab3.Domain.Services.CategoryService;

namespace WEB_353504_Antonava_Lab3.Domain.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private List<Product> _products = new();
        private List<Category> _categories;
        private readonly int _itemsPerPage;

        public MemoryProductService(IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data!;
            _itemsPerPage = int.TryParse(config["ItemsPerPage"], out var value) ? value : 3;
            SetupData();
        }

        private void SetupData()
        {
            _products = new List<Product>
            {
                // Корма
                new Product
                {
                    Id = 1,
                    Name = "Сухой корм для кошек",
                    Description = "Корм премиум-класса с курицей",
                    Weight = 2.5m,
                    ImagePath = "images/cat-food.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "food")
                },
                new Product
                {
                    Id = 2,
                    Name = "Сухой корм для собак",
                    Description = "Говядина и рис, для взрослых собак",
                    Weight = 3.0m,
                    ImagePath = "images/dog-food.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "food")
                },

                // Игрушки
                new Product
                {
                    Id = 3,
                    Name = "Игрушка для собак",
                    Description = "Мягкая игрушка с пищалкой",
                    Weight = 0.3m,
                    ImagePath = "images/dog-toy.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "toys")
                },
                new Product
                {
                    Id = 4,
                    Name = "Мячик для кошек",
                    Description = "Лёгкий мячик с колокольчиком",
                    Weight = 0.1m,
                    ImagePath = "images/cat-ball.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "toys")
                },

                // Аксессуары
                new Product
                {
                    Id = 5,
                    Name = "Ошейник для собак",
                    Description = "Регулируемый, с мягкой подкладкой",
                    Weight = 0.2m,
                    ImagePath = "images/dog-collar.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "accessories")
                },
                new Product
                {
                    Id = 6,
                    Name = "Переноска для кошек",
                    Description = "Пластиковая, с вентиляцией",
                    Weight = 1.5m,
                    ImagePath = "images/cat-carrier.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "accessories")
                },

                // Средства ухода
                new Product
                {
                    Id = 7,
                    Name = "Шампунь для собак",
                    Description = "Гипоаллергенный, без запаха",
                    Weight = 0.5m,
                    ImagePath = "images/dog-shampoo.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "care-products")
                },
                new Product
                {
                    Id = 8,
                    Name = "Щётка для кошек",
                    Description = "С мягкими зубьями, для длинной шерсти",
                    Weight = 0.2m,
                    ImagePath = "images/cat-brush.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "care-products")
                },

                // Лакомства
                new Product
                {
                    Id = 9,
                    Name = "Лакомство для собак",
                    Description = "Сушёные говяжьи лёгкие",
                    Weight = 0.25m,
                    ImagePath = "images/dog-treat.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "treats")
                },
                new Product
                {
                    Id = 10,
                    Name = "Лакомство для кошек",
                    Description = "Куриные полоски с кошачьей мятой",
                    Weight = 0.15m,
                    ImagePath = "images/cat-treat.jpg",
                    ImageMimeType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName == "treats")
                }
            };
        }

        public Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var filtered = string.IsNullOrEmpty(categoryNormalizedName)
                ? _products
                : _products.Where(p => p.Category?.NormalizedName == categoryNormalizedName).ToList();

            var totalItems = filtered.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / _itemsPerPage);

            var pagedItems = filtered
                .Skip((pageNo - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            var model = new ListModel<Product>
            {
                Items = pagedItems,
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            return Task.FromResult(ResponseData<ListModel<Product>>.Success(model));
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id) => throw new NotImplementedException();
        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile) => throw new NotImplementedException();
        public Task DeleteProductAsync(int id) => throw new NotImplementedException();
        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile) => throw new NotImplementedException();
    }
}
