using AutoMapper;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.ViewComponents
{
    public class RandomSellerViewComponent:ViewComponent
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private const int RandomProductsNumber = 6;
        public RandomSellerViewComponent(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var randomProducts = await _productRepository.GetRandomProductsAsync(RandomProductsNumber);
            var viewmodelRandomProducts = _mapper.Map<List<ProductListViewModel>>(randomProducts);

            return View("RandomSeller", viewmodelRandomProducts);
        }
    }
}
