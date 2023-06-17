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
        Task<Product> GetTestAsync(int id);
    }
}
