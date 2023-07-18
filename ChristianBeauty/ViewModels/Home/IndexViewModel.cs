using ChristianBeauty.ViewModels.Products;

namespace ChristianBeauty.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<ProductListViewModel> RandomProducts { get; set; }
        public List<ProductListViewModel> TopSellerProducts { get; set; }
    }
}
