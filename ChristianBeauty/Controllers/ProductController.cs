using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Marterials;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Controllers
{
    public class ProductController : Controller
    {
        private protected IProductRepository _productRepository;
        private protected ICategoryRepository _categoryRepository;
        private protected IMaterialRepository _materialRepository;

        private readonly IMapper _mapper;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMaterialRepository materialRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _materialRepository = materialRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productRepository.GetProductWithImagesEagerLoadAsync(id);
            var category = await _categoryRepository.GetCategoryAndSubNameAsync(product.CategoryId);
            var material = await _materialRepository.GetAsync(product.MaterialId);
            var viewModel = _mapper.Map<GetProductViewModel>(product);
            return View(viewModel);
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var product = await _productRepository.GetAllProductWithImagesEagerLoadAsync();
            var viewModel = _mapper.Map<List<AllProductsViewModel>>(product);
            return View(viewModel);
        }
    }
}
