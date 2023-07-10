using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Products
{
    public class ProductDetailViewModel
    {
        public GetProductViewModel ProductDetail { get; set; }
        public List<ProductListViewModel> SimilarProducts { get; set; }
    }
}
