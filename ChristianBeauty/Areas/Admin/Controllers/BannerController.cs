using AutoMapper;
using ChristianBeauty.Data.Interfaces.Banners;
using ChristianBeauty.Data.Interfaces.Blogs;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Data.Repositories.Products;
using ChristianBeauty.Models;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Banners;
using ChristianBeauty.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class BannerController: AdminBaseController
    {
        private protected IBannerRepositroy _repository;
        private readonly IMapper _mapper;
        private protected IProductRepository _productrepository;

        public BannerController(IBannerRepositroy blogsRepository, IProductRepository productRepository, IMapper mapper)
        {
            _repository = blogsRepository;
            _productrepository = productRepository;    
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Add(AddBannerViewModel model)
        {
            var getProduct = await _productrepository.GetProductWithImagesEagerLoadAsync(model.ProductId);
            if(getProduct == null)
            {
                return RedirectToAction("Index");
            }
            var galleryImage = getProduct.Gallery?.FirstOrDefault();

            Banner banner = new Banner
            {
                ProductId = model.ProductId,
                Image =   galleryImage.ImageName,
                CreateDate = DateTime.Now,
            };
            await _repository.AddAsync(banner);
            await _repository.SaveAsync();
            return RedirectToAction("Index","Product");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _repository.GetBannerByProductId(id);
            if (banner != null)
            {
                _repository.Remove(banner);
                await _repository.SaveAsync();
            }
            return RedirectToAction("Index", "Product");

        }
    }
}
