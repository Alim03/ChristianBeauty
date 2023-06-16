using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        private protected IMapper _mapper;

        public ProductController(
            IProductRepository repository,
            IMaterialRepository materialRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _materialRepository = materialRepository;
            _mapper = mapper;
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
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            var addProductViewModel = new AddProductViewModel
            {
                Materials = materialsSelectListItem.ToList()
            };
            return View(addProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);
                product.CategoryId = 1;
                await _repository.AddAsync(product);
                await _repository.SaveAsync();
                return RedirectToAction("Index");
            }
            var material = await _materialRepository.GetAllAsync();
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            viewModel.Materials = materialsSelectListItem.ToList();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _repository.GetAsync(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var viewmodel = _mapper.Map<EditProductViewModel>(product);
            var material = await _materialRepository.GetAllAsync();
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            viewmodel.Materials = materialsSelectListItem.ToList();
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel viewModel)
        {
            var material = await _materialRepository.GetAllAsync();
            var materialsSelectListItem = SelectListHelper.ConvertMaterialToSelectListItems(
                material.ToList()
            );
            if (!ModelState.IsValid)
            {
                viewModel.Materials = materialsSelectListItem.ToList();
                return View(viewModel);
            }
            var product = await _repository.GetAsync(viewModel.Id);
            if (product != null)
            {
                _mapper.Map(viewModel, product);
                _repository.Update(product);
                await _repository.SaveAsync();
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
    }
}
