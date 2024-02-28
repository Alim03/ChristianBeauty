using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Banners;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Gallery;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Data.Repositories.Products;
using ChristianBeauty.Models;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Common;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private protected IProductRepository _repository;
        private protected IMaterialRepository _materialRepository;
        private protected ICategoryRepository _categoryRepository;
        private protected IGalleryRepository _galleryRepository;
        private protected IBannerRepositroy _bannerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private protected IMapper _mapper;
        private const int PAGESIZE = 10;


        public ProductController(
            IProductRepository repository,
            IMaterialRepository materialRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IGalleryRepository galleryRepository,
            IBannerRepositroy bannerRepositroy,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _repository = repository;
            _materialRepository = materialRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _galleryRepository = galleryRepository;
            _bannerRepository = bannerRepositroy;
            _webHostEnvironment = webHostEnvironment;
        }


        
        public async Task<IActionResult> Index(int page = 1,
            int? categoryId = null,
            string? searchKey = null,
            int? material = null,
            int? subcategory = null,
            int? has_selling_stock = null,
            bool? HasConfiguredAsBanner= null
           )
        
        {
            List<Product> products;
            int totalCount;
            if (categoryId != null || material != null ||  searchKey != null || has_selling_stock != null && HasConfiguredAsBanner !=null)
            {
                products = await _repository.GetAllProductWithImagesEagerByFilterLoadAsync(
                    page,
                    searchKey,
                    PAGESIZE,
                    categoryId,
                    material,
                    subcategory,
                    has_selling_stock,
                    HasConfiguredAsBanner
                );

                totalCount = await _repository.GetTotalCountProductsByFilterAsync(
                    searchKey,
                    categoryId,
                    material,
                    subcategory,
                    has_selling_stock,
                    HasConfiguredAsBanner
                );
            }
            else
            {
                products = await _repository.GetAllProductWithImagesEagerLoadAsync(page, PAGESIZE);
                totalCount = await _repository.GetTotalCountProductsAsync();
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

            //var products = await _repository.GetAllProductWithImagesEagerLoadAsync();
            var productsModel = _mapper.Map<List<ProductViewModel>>(products);
            foreach (var product in productsModel)
            {
                product.HasBannerConfigured = await _bannerRepository.CheckProductHasConfiguredAsBanner(product.Id);
            }

            var viewModel = new AdminPaginatedProductsViewModel
            {
                Products = productsModel,
                Metadata = paginationMetadata,
                Categories = categoriesSelectListItem,
                Materials = materialssSelectListItem
            };
            return View(viewModel);
        }


         [HttpGet]
        public async Task<IActionResult> Add()
        {
            var material = await _materialRepository.GetAllAsync();
            var category = await _categoryRepository.GetAllParentCategoriesAsync();
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                category.ToList()
            );

            var addProductViewModel = new AddProductViewModel
            {
                Materials = materialsSelectListItem.ToList(),
                Categories = categoriesSelectListItem.ToList()
            };
            return View(addProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);
                if (viewModel.SelectedSubCategoryId != null)
                {
                    _repository.AddSubcategoryToProduct(
                        product,
                        viewModel.SelectedSubCategoryId.Value
                    );
                }
                await _repository.AddAsync(product);
                await _galleryRepository.SaveAsync();

                foreach (var gallery in viewModel.Gallery)
                {
                    Gallery galllery = new Gallery
                    {
                        ImageName = await FileHandler.ImageUploadAsync(gallery, _webHostEnvironment),
                        ProductId = product.Id
                    };
                    await _galleryRepository.AddAsync(galllery);
                    await _galleryRepository.SaveAsync();
                }
                await _galleryRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            var material = await _materialRepository.GetAllAsync();
            var category = await _categoryRepository.GetAllParentCategoriesAsync();

            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                category.ToList()
            );

            viewModel.Materials = materialsSelectListItem.ToList();
            viewModel.Categories = categoriesSelectListItem.ToList();
            return View(viewModel);
        }   

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var product = await _repository.GetAsync(id);
            var product = await _repository.GetProductWithImagesEagerLoadAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var isSubCategory = await _categoryRepository.IsSubCategoryAsync(product.CategoryId);

            var viewmodel = _mapper.Map<EditProductViewModel>(product);
            if (isSubCategory)
            {
                viewmodel.SelectedSubCategoryId = product.CategoryId;
                var parentCategoryId = await _categoryRepository.GetSubCategoryParentAsync(
                    product.CategoryId
                );
                viewmodel.SelectedCategoryId = parentCategoryId;
                var subCategories = _categoryRepository.GetAllParentsSubCategories(
                    parentCategoryId
                );
                var subCategoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                    subCategories.ToList()
                );
                viewmodel.SubCategories = subCategoriesSelectListItem;
            }
            else
            {
                var parentsSubcategories = _categoryRepository.GetAllParentsSubCategories(
                    product.CategoryId
                );
                var subCategoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                    parentsSubcategories.ToList()
                );
                viewmodel.SubCategories = subCategoriesSelectListItem;
            }
            var material = await _materialRepository.GetAllAsync();
            var category = await _categoryRepository.GetAllParentCategoriesAsync();
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                category.ToList()
            );
            viewmodel.Materials = materialsSelectListItem.ToList();
            viewmodel.Categories = categoriesSelectListItem.ToList();
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            EditProductViewModel viewModel,
            IFormFileCollection Gallery,
            List<int> ChangedImagesIds
        )
        {
            var material = await _materialRepository.GetAllAsync();
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            var category = await _categoryRepository.GetAllParentCategoriesAsync();
            var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
                category.ToList()
            );
            if (!ModelState.IsValid)
            {
                viewModel.Materials = materialsSelectListItem.ToList();
                viewModel.Categories = categoriesSelectListItem.ToList();
                return View(viewModel);
            }
            if (viewModel.SelectedSubCategoryId != null)
            {
                viewModel.SelectedCategoryId = viewModel.SelectedSubCategoryId.Value;
            }

            var product = await _repository.GetAsync(viewModel.Id);
            if (product != null)
            {
                _mapper.Map(viewModel, product);
                _repository.Update(product);
                await _repository.SaveAsync();
            }

           if(await _bannerRepository.CheckProductHasConfiguredAsBanner(viewModel.Id))
            {
                 var res = await _bannerRepository.GetBannerByProductId(viewModel.Id);
                _bannerRepository.Remove(res);
                await _galleryRepository.SaveAsync();
            }
            if (Gallery.Count > 0)
            {
                foreach (var id in ChangedImagesIds)
                {
                    if(id != 0)
                    {
                    var image = await _galleryRepository.GetImageByIdAsync(id);
                    _galleryRepository.Remove(image);
                    await _galleryRepository.SaveAsync();
                    FileHandler.DeleteImage(image.ImageName,_webHostEnvironment);
                    }
                }
                foreach (var item in Gallery)
                {
                    Gallery galllery = new Gallery
                    {
                        ImageName = await FileHandler.ImageUploadAsync(item,_webHostEnvironment),
                        ProductId = product.Id
                    };
                    await _galleryRepository.AddAsync(galllery);
                    await _galleryRepository.SaveAsync();
                }
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetAsync(id);
            var banner = await _bannerRepository.GetBannerByProductId(id);
            if (product != null)
            {
                _repository.Remove(product);
                if(banner != null)
                {
                _bannerRepository.Remove(banner);
                }
                await _repository.SaveAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetSubCategories(int categoryId)
        {
            var subcategories = _categoryRepository.GetAllParentsSubCategories(categoryId);
            return Json(subcategories);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var image = await _galleryRepository.GetAsync(imageId);
            if (image != null)
            {
                _galleryRepository.Remove(image);
                await _repository.SaveAsync();
                FileHandler.DeleteImage(image.ImageName, _webHostEnvironment);
                return RedirectToAction("Edit", new { id = image?.ProductId });
            }
            return RedirectToAction("Index");

        }


    }
}
