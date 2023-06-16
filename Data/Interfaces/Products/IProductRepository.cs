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
    }
}
