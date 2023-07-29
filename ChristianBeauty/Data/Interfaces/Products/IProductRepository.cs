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
        Task<List<Product>> GetAllProductWithImagesEagerLoadAsync();

        Task<List<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<List<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize, string query);
        Task<int> GetTotalCountProductsAsync();
        Task<int> GetTotalCountProductsAsync(string query);
        Task<int> GetTotalCountProductsByFilterAsync(
            int? categoryId,
            int? materialId,
            int? subcategory,
            int? has_selling_stock
        );
        Task<List<Product>> GetPaginatedProductsByFilterAsync(
            int pageNumber,
            int pageSize,
            int? categoryId,
            int? materialId,
            int? subcategory,
            int? has_selling_stock
        );
        Task<List<Product>> GetProductsByCategoryWithLimitAsync(
            int categoryId,
            int limit,
            int excludedProductId
        );
        Task<List<Product>> GetRandomProductsAsync(int number);
        Task<List<Product>> GetTopSellerProductsByLimit(int limit);

        Task<Product> GetTestAsync(int id);
    }
}
