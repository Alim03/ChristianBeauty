using AutoMapper;
using ChristianBeauty.Data.Interfaces.Banners;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Banners;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.ViewComponents
{
    public class SliderViewComponent: ViewComponent
    {
        private readonly IBannerRepositroy _bannerRepositroy;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private const int RandomProductsNumber = 6;
        public SliderViewComponent(IBannerRepositroy bannerRepositroy, IProductRepository productRepository, IMapper mapper)
        {
            _bannerRepositroy = bannerRepositroy;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banners = await _bannerRepositroy.GetAllAsync();
            List<GetBannerWithDetailViewModel> getBanners = new List<GetBannerWithDetailViewModel>();
            foreach (var item in banners)
            {
                var product = await _productRepository.GetProductWithCategoryEagerLoadAsync(item.ProductId);
                GetBannerWithDetailViewModel withDetailViewModel = new GetBannerWithDetailViewModel
                {
                    CategoryName = product.Category.Name,
                    Image = item.Image,
                    ProductId = item.ProductId,
                    CreateDate = item.CreateDate,
                    Tittle = product.Name,
                    Id = item.Id,
                };
                getBanners.Add(withDetailViewModel);
            }
            //var bannersViewModel = _mapper.Map<List<BannersViewModel>>(banners);
            
            return View("Slider", getBanners);
        }
    }
}
