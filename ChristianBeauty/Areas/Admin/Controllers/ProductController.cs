using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Gallery;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Models;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Products;
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


        private protected IMapper _mapper;

        public ProductController(
            IProductRepository repository,
            IMaterialRepository materialRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IGalleryRepository galleryRepository

        )
        {
            _repository = repository;
            _materialRepository = materialRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _galleryRepository = galleryRepository;

        }

        public IActionResult Index()
        {
            var products = _repository.GetAllRowsSelectedFields();
            var viewmodel = _mapper.Map<List<ProductViewModel>>(products);
            return View(viewmodel);
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
                await _repository.SaveAsync();
                
                foreach (var gallery in viewModel.Gallery)
                {
                    Gallery galllery = new Gallery { ImageName = await FileHandler.ImageUploadAsync(gallery), ProductId = product.Id };
                    await _galleryRepository.AddAsync(galllery);
                    await _galleryRepository.SaveAsync();
                }
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
        public async Task<IActionResult> Edit(EditProductViewModel viewModel, IFormFileCollection Gallery)
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
            var gallleries = await _galleryRepository.GetProductImagesByIdAsync(viewModel.Id);
            foreach (var item in gallleries)
            {
                _galleryRepository.Remove(item);
                await _galleryRepository.SaveAsync();
                FileHandler.DeleteImage(item.ImageName);


            }
            foreach (var item in Gallery)
            {
                Gallery galllery = new Gallery { ImageName = await FileHandler.ImageUploadAsync(item), ProductId = product.Id };
                await _galleryRepository.AddAsync(galllery);
                await _galleryRepository.SaveAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetAsync(id);
            if (product != null)
            {
                _repository.Remove(product);
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
    }
}
