using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Marterials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class MaterialController : AdminBaseController
    {
        private protected IMaterialRepository _repository;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var allMaterial = await _repository.GetAllAsync();
            var viewModel = _mapper.Map<List<MaterialViewModel>>(allMaterial);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var material = new Material() { Name = viewModel.MaterialName };
                await _repository.AddAsync(material);
                await _repository.SaveAsync();
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _repository.GetAsync(id);
            if (material == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new EditMaterialViewModel() { Name = material.Name };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMaterialViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var material = new Material() { Id = viewModel.Id, Name = viewModel.Name };
            _repository.Update(material);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var material = await _repository.GetAsync(id);
            if (material != null)
            {
                _repository.Remove(material);
                await _repository.SaveAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
