using AutoMapper;
using ChristianBeauty.Data.Interfaces.Blogs;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Repositories.Materials;
using ChristianBeauty.Models;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Blogs;
using ChristianBeauty.ViewModels.Common;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.Controllers
{
    public class BlogController : Controller
    {
        private protected IBlogsRepository _blogsRepository;
        private readonly IMapper _mapper;
        private const int PAGESIZE = 1;

        public BlogController(IBlogsRepository blogsRepository, IMapper mapper)
        {
            _blogsRepository = blogsRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            List<Blog> blogs;
            int totalCount;

            blogs = await _blogsRepository.GetPaginatedBlogssAsync(page, PAGESIZE);
            totalCount = await _blogsRepository.CountBlogsAsync();

            PaginationMetadata paginationMetadata = new PaginationMetadata
            {
                TotalCount = totalCount,
                PageSize = PAGESIZE,
                CurrentPage = page
            };
            var viewModel = new PaginatedBlogsViewModel
            {
                Blogs = blogs,
                Metadata = paginationMetadata,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogsRepository.GetAsync(id);
            var similarBlogs = await _blogsRepository.GetRandomBlogAsync(3, id);
            var blogDetailViewModel = new BlogDetailViewModel
            {
                Blog = blog,
                SimilarBlogs = similarBlogs
            };
            return View(blogDetailViewModel);
        }
    }
}
