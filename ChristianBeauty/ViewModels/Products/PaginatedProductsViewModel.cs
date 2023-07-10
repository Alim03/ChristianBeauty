using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristianBeauty.ViewModels.Products
{
    public class PaginatedProductsViewModel
    {
        public List<Product> Products { get; set; }
        public PaginationMetadata Metadata { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Materials { get; set; }
    }
}
