using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Products
{
    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
        public int? Size { get; set; }
        public decimal? Weight { get; set; }
        public string Image { get; set; }
    }
}
