using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.ViewComponents
{
    public class TopSellerViewComponent: ViewComponent
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private const int TopSellerProductsNumber = 6;
        public TopSellerViewComponent(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topSellerProducts = await _productRepository.GetTopSellerProductsByLimit(TopSellerProductsNumber
    );

            var viewmodelTopSellerProducts = _mapper.Map<List<ProductListViewModel>>(topSellerProducts);
            return View("TopSeller", viewmodelTopSellerProducts);
        }

    }
}
