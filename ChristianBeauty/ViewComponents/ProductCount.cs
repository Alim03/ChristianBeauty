using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.ViewComponents
{
    public class ProductCount: ViewComponent
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductCount(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _productRepository.GetCountProductsAsync();
            return View("ProductCount", categories);
        }
    }
}
