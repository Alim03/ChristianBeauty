using AutoMapper;
using ChristianBeauty.Data.Interfaces.Blogs;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Models;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Blogs;
using ChristianBeauty.ViewModels.Categories;
using ChristianBeauty.ViewModels.Marterials;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class BlogController : AdminBaseController
    {
        private protected IBlogsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(IBlogsRepository blogsRepository, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _repository = blogsRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAllAsync();
            var viewModel = _mapper.Map<List<BlogsViewModel>>(result);
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogViewModel model)
        {
            //var result = _mapper.Map<Blog>(model);
            Blog blog = new Blog
            {
                Description = model.Description,
                Tittle = model.Tittle,
                ReadingTime = model.ReadingTime,
                Image = await FileHandler.ImageUploadAsync(model.Image,_webHostEnvironment),
                CreateDate = DateTime.Now,
                Labels = model.Labels
            };
            await _repository.AddAsync(blog);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _repository.GetAsync(id);
            if (blog == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new EditBlogViewModel() { Labels =blog.Labels, Description = blog.Description, Tittle = blog.Tittle, Image = blog.Image , ReadingTime = blog.ReadingTime };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogViewModel viewModel)
        {
            var blog = await _repository.GetAsync(viewModel.Id);
            blog.Description = viewModel.Description;
            blog.ReadingTime = viewModel.ReadingTime;
            blog.Tittle = viewModel.Tittle;
            blog.Labels = viewModel.Labels;
            blog.Image = viewModel.ImageFile == null ? blog.Image : await FileHandler.ImageUploadAsync(viewModel.ImageFile,_webHostEnvironment);
            _repository.Update(blog);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _repository.GetAsync(id);
            if (blog != null)
            {
                _repository.Remove(blog);
                await _repository.SaveAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
