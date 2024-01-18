using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristianBeauty.ViewModels.Products
{
    public class AdminPaginatedProductsViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public PaginationMetadata Metadata { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Materials { get; set; }
    }
}
