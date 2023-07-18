using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryViewComponent(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAllParentCategoriesAsync();
            var viewModel = _mapper.Map<List<GetAllCategoryViewModel>>(categories);
            return View("Category", viewModel);
        }
    }
}
