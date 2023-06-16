using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Models;

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
    }
}
