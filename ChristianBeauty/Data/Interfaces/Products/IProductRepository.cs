using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Interfaces.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllRowsSelectedFields();
        void AddSubcategoryToProduct(Product product, int subcategoryId);
        void AddSubcategoryToProduct(int productId, int subcategoryId);
        Task<Product> GetProductWithImagesEagerLoadAsync(int id);
        Task<List<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<List<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize, string query);
        Task<int> GetTotalCountProductsAsync();
        Task<int> GetTotalCountProductsAsync(string query);
        Task<int> GetTotalCountProductsByCategoryAsync(int categoryId);
        Task<List<Product>> GetPaginatedProductsByCategoryAsync(
            int pageNumber,
            int pageSize,
            int categoryId
        );
        Task<Product> GetTestAsync(int id);
    }
}
