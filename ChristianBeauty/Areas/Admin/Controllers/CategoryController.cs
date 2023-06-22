using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private protected ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var allCategories = await _repository.GetAllParentCategoriesEagerLoadAsync();
            var viewModel = _mapper.Map<List<CategoryViewModel>>(allCategories);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category { Name = viewModel.Name };
                await _repository.AddAsync(category);
                await _repository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddSubCategory(int parentCategoryId)
        {
            var viewModel = new AddSubCategoryViewModel { ParentCategoryId = parentCategoryId };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(AddSubCategoryViewModel viewModel)
        {
            var parentCategory = await _repository.GetAsync(viewModel.ParentCategoryId);
            if (parentCategory != null)
            {
                var category = new Category { Name = viewModel.Name };
                category.ParentCategory = parentCategory;
                await _repository.AddAsync(category);
                await _repository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, int? parentId)
        {
            var cateogry = await _repository.GetAsync(id);
            if (cateogry == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new EditCategoryViewModel() { Name = cateogry.Name };
            if (parentId != null)
            {
                viewModel.ParentCategoryId = parentId;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var category = new Category() { Id = viewModel.Id, Name = viewModel.Name };
            if (viewModel.ParentCategoryId != null)
            {
                var parentCategory = await _repository.GetAsync(viewModel.ParentCategoryId.Value);
                if (parentCategory != null)
                {
                    category.ParentCategory = parentCategory;
                }
            }
            _repository.Update(category);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetAsync(id);
            if (category != null)
            {
                _repository.Remove(category);
                await _repository.SaveAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
