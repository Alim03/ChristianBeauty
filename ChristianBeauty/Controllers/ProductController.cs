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
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Common;
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
        private const int PAGESIZE = 1;

        private readonly IMapper _mapper;

        public ProductController(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMaterialRepository materialRepository,
            IMapper mapper
        )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _materialRepository = materialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id != null || id != 0)
            {

                var product = await _productRepository.GetProductWithImagesEagerLoadAsync(id);
                var similarProduct = await _productRepository.GetProductsByCategoryWithLimitAsync(
                    product.CategoryId,
                    3,
                    id
                );
                var similarProductViewModel = _mapper.Map<List<ProductListViewModel>>(similarProduct);
                var productViewModel = _mapper.Map<GetProductViewModel>(product);
                var productDetailViewModel = new ProductDetailViewModel
                {
                    ProductDetail = productViewModel,
                    SimilarProducts = similarProductViewModel
                };
                return View(productDetailViewModel);
            }

            else
                return View();
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int? categoryId = null,
            int? material = null
        )
        {
            List<Product> products;
            int totalCount;
            if (categoryId != null || material != null)
            {
                products = await _productRepository.GetPaginatedProductsByFilterAsync(
                    page,
                    PAGESIZE,
                    categoryId,
                    material
                );
                totalCount = await _productRepository.GetTotalCountProductsByFilterAsync(
                    categoryId,
                    material
                );
            }
            else
            {
                products = await _productRepository.GetPaginatedProductsAsync(page, PAGESIZE);
                totalCount = await _productRepository.GetTotalCountProductsAsync();
            }

            var category = await _categoryRepository.GetAllParentCategoriesAsync();
            var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                category.ToList()
            );

            var materials = await _materialRepository.GetAllAsync();
            var materialssSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                materials.ToList()
            );
            PaginationMetadata paginationMetadata = new PaginationMetadata
            {
                TotalCount = totalCount,
                PageSize = PAGESIZE,
                CurrentPage = page
            };
            var viewModel = new PaginatedProductsViewModel
            {
                Products = products,
                Metadata = paginationMetadata,
                Categories = categoriesSelectListItem,
                Materials = materialssSelectListItem
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            var products = await _productRepository.GetPaginatedProductsAsync(page, PAGESIZE, q);
            var totalCount = await _productRepository.GetTotalCountProductsAsync(q);
            var category = await _categoryRepository.GetAllParentCategoriesAsync();
            var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                category.ToList()
            );

            var materials = await _materialRepository.GetAllAsync();
            var materialssSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                materials.ToList()
            );
            PaginationMetadata paginationMetadata = new PaginationMetadata
            {
                TotalCount = totalCount,
                PageSize = PAGESIZE,
                CurrentPage = page
            };
            var viewModel = new PaginatedProductsViewModel
            {
                Products = products,
                Metadata = paginationMetadata,
                Categories = categoriesSelectListItem,
                Materials = materialssSelectListItem
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult GetSubCategories(int categoryId)
        {
            var subcategories = _categoryRepository.GetAllParentsSubCategories(categoryId);
            return Json(subcategories);
        }
    }
}
