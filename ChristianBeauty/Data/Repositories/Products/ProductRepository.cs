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

        public async Task<Product> GetProductWithImagesEagerLoadAsync(int id)
        {
            return await Context.Products
                .Include(c => c.Gallery)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Product>> GetAllProductWithImagesEagerLoadAsync()
        {
            return await Context.Products.Include(c => c.Gallery).ToListAsync();
        }

        public async Task<List<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Product> products = await Context.Products
                .Include(p => p.Gallery)
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList()
                        }
                )
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetPaginatedProductsAsync(
            int pageNumber,
            int pageSize,
            string query
        )
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Product> products = await Context.Products
                .Include(p => p.Gallery)
                .Where(x => x.Name.Contains(query))
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList()
                        }
                )
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetPaginatedProductsByFilterAsync(
            int pageNumber,
            int pageSize,
            int? categoryId,
            int? materialId
        )
        {
            int skip = (pageNumber - 1) * pageSize;

            IQueryable<Product> query = Context.Products;

            if (categoryId != null)
            {
                query = query.Where(
                    x =>
                        x.CategoryId == categoryId.Value
                        || x.Category.ParentCategoryId == categoryId.Value
                );
            }

            if (materialId != null)
            {
                query = query.Where(x => x.MaterialId == materialId.Value);
            }

            return await query
                .Include(p => p.Gallery)
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList()
                        }
                )
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountProductsAsync()
        {
            return await Context.Products.CountAsync();
        }

        public async Task<int> GetTotalCountProductsAsync(string query)
        {
            return await Context.Products.Where(x => x.Name.Contains(query)).CountAsync();
        }

        public async Task<int> GetTotalCountProductsByFilterAsync(int? categoryId, int? materialId)
        {
            IQueryable<Product> query = Context.Products;

            if (categoryId != null)
            {
                query = query.Where(
                    x =>
                        x.CategoryId == categoryId.Value
                        || x.Category.ParentCategoryId == categoryId.Value
                );
            }

            if (materialId != null)
            {
                query = query.Where(x => x.MaterialId == materialId.Value);
            }
            return await query.CountAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryWithLimitAsync(
            int categoryId,
            int limit,
            int excludedProductId
        )
        {
            var products = await Context.Products
                .Where(
                    x =>
                        (x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
                        && x.Id != excludedProductId
                )
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList()
                        }
                )
                .Take(limit)
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetRandomProductsAsync(int number)
        {
            return await Context.Products
                .OrderBy(r => Guid.NewGuid())
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList()
                        }
                )
                .Take(number)
                .ToListAsync();
        }
    }
}
