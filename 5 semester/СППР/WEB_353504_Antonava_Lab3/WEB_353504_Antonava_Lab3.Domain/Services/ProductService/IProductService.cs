using Microsoft.AspNetCore.Http;
using WEB_353504_Antonava_Lab3.Domain.Entities;
using WEB_353504_Antonava_Lab3.Domain.Models;

namespace WEB_353504_Antonava_Lab3.Domain.Services.ProductService
{
    public interface IProductService
    {
        Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        Task<ResponseData<Product>> GetProductByIdAsync(int id);
        Task UpdateProductAsync(int id, Product product, IFormFile? formFile);
        Task DeleteProductAsync(int id);
        Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile);
    }
}
