using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristianBeauty.Data.Repositories.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ChristianBeautyDbContext context) : base(context) { }

        public IEnumerable<Product> GetAllRowsSelectedFields()
        {
            return Context.Products
                .Select(
                    x =>
                        new Product
                        {
                            Id = x.Id,
                            Name = x.Name,
                            ProductCode = x.ProductCode
                        }
                )
                .ToList();
        }

        public void AddSubcategoryToProduct(Product product, int subcategoryId)
        {
            var subcategory = Context.Categories.FirstOrDefault(c => c.Id == subcategoryId);

            if (product != null && subcategory != null)
            {
                product.Category = subcategory;
            }
        }

        public void AddSubcategoryToProduct(int productId, int subcategoryId)
        {
            var product = Context.Products.FirstOrDefault(p => p.Id == productId);
            var subcategory = Context.Categories.FirstOrDefault(c => c.Id == subcategoryId);

            if (product != null && subcategory != null)
            {
                product.Category = subcategory;
            }
        }

        public async Task<Product> GetTestAsync(int id)
        {
            return await Context.Products
                .Include(p => p.Category)
                .ThenInclude(c => c.Subcategories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
