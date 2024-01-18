using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Products;
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

        public async Task<Product> GetProductWithImagesEagerLoadAsync(int id)
        {
            return await Context.Products
                .Include(c => c.Gallery)
                .Include(c => c.Material)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Product>> GetAllProductWithImagesEagerLoadAsync(int pageNumber,
            int pageSize
         )
        {
            int skip = (pageNumber - 1) * pageSize;
            return await Context.Products.Include(c => c.Gallery).Include(p => p.Category).Skip(skip)
                .Take(pageSize).ToListAsync();
        }
        public async Task<List<Product>> GetAllProductWithImagesEagerByFilterLoadAsync(int pageNumber, string? searchKey, int pageSize, int? categoryId,
            int? materialId,
            int? subcategory,
            int? has_selling_stock,
            bool? HasConfiguredAsBanner)
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

            if(searchKey !=null)
            {
                query = query.Where(x => x.Name.Contains(searchKey));
            }

            if (materialId != null)
            {
                query = query.Where(x => x.MaterialId == materialId.Value);
            }

            if (subcategory != null)
            {
                query = query.Where(x => x.CategoryId == subcategory.Value);
            }
            if (has_selling_stock != null)
            {
                if (has_selling_stock.Value == 1)
                {
                    query = query.Where(x => x.IsFinished != null);
                }
                if (has_selling_stock.Value == 2)
                {
                    query = query.Where(x => x.IsFinished == false);
                }
                if (has_selling_stock.Value == 3)
                {
                    query = query.Where(x => x.IsFinished == true);
                }

            }


            if (HasConfiguredAsBanner != null)
            {
                if (HasConfiguredAsBanner.Value == true)
                {
                    foreach (var item in query)
                    {
                        query = query.Where(product => Context.Banners.Any(banner => banner.ProductId == item.Id));
                    }
                }
            }
            int skip = (pageNumber - 1) * pageSize;
            return await query.Include(c => c.Gallery).Include(p => p.Category).Skip(skip)
                .Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Product> products = await Context.Products
                .Include(p => p.Gallery).Include(x=>x.Category)
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList(),
                            Category = p.Category,
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
            int? materialId,
            int? subcategory,
            int? has_selling_stock
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

            if (subcategory != null)
            {
                query = query.Where(x => x.CategoryId == subcategory.Value);
            }
            if (has_selling_stock != null)
            {
                if (has_selling_stock.Value == 1)
                {
                    query = query.Where(x => x.IsFinished != null);
                }
                if (has_selling_stock.Value==2)
                {
                    query = query.Where(x => x.IsFinished == false);
                }
                if (has_selling_stock.Value == 3)
                {
                    query = query.Where(x => x.IsFinished == true);
                }

            }
            return await query
                .Include(p => p.Gallery).Include(p => p.Category)
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList(),
                            Category = p.Category,
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

        public async Task<int> GetTotalCountProductsByFilterAsync(
            string searchKey,
            int? categoryId,
            int? materialId,
            int? subcategory,
            int? has_selling_stock,
            bool? HasConfiguredAsBanner
        )
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

            if (searchKey != null)
            {
                query = query.Where(x => x.Name.Contains(searchKey));
            }

            if (materialId != null)
            {
                query = query.Where(x => x.MaterialId == materialId.Value);
            }
            if (subcategory != null)
            {
                query = query.Where(x => x.CategoryId == subcategory.Value);
            }
            if (has_selling_stock != null)
            {
                if (has_selling_stock.Value == 1)
                {
                    query = query.Where(x => x.IsFinished != null);
                }
                if (has_selling_stock.Value == 2)
                {
                    query = query.Where(x => x.IsFinished == true);
                }
                if (has_selling_stock.Value == 3)
                {
                    query = query.Where(x => x.IsFinished == false);
                }

            }
            if (HasConfiguredAsBanner == true)
            {
                foreach (var item in query)
                {
                    query = query.Where(product => Context.Banners.Any(banner => banner.ProductId == item.Id));
                }
            }


            return await query.CountAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryWithLimitAsync(
            int categoryId,
            int limit,
            int excludedProductId
        )
        {
            var products = await Context.Products.Include(x=>x.Category)
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
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList(),
                            Category = p.Category
                        }
                )
                .Take(limit)
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetRandomProductsAsync(int number)
        {
            return await Context.Products.Include(x=>x.Category)
                .OrderBy(r => Guid.NewGuid())
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList(),
                            Category = p.Category,
                        }
                )
                .Take(number)
                .ToListAsync();
        }

        public async Task<List<Product>> GetTopSellerProductsByLimit(int limit)
        {
            return await Context.Products
                .Include(x=>x.Category)
                .Where(x => x.IsTopSeller == true)
                .Select(
                    p =>
                        new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Gallery = p.Gallery.OrderBy(g => g.Id).Take(1).ToList(),
                            Category = p.Category,
                        }
                )
                .Take(limit)
                .ToListAsync();
        }

        public async Task<ProductCountViewModel> GetCountProductsAsync()
        {
            var totoal = await Context.Products.CountAsync();
            var enable = await Context.Products.CountAsync(x=>x.IsEnable == true);
            var disabled = await Context.Products.CountAsync(x=>x.IsEnable == false);
            ProductCountViewModel productCountViewModel = new ProductCountViewModel
            {
                DisabledProduct = disabled,
                EnabledProduct = enable,
                Total = totoal
            };
            return productCountViewModel;

        }

        public async Task<Product> GetProductWithCategoryEagerLoadAsync(int id)
        {
            return await Context.Products
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Product>> GetProductBySearch(string searchKey)
        {
            throw new NotImplementedException();
        }
    }
}
