using AutoMapper;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Blogs;
using ChristianBeauty.ViewModels.Categories;

namespace ChristianBeauty.Profiles
{
    public class BlogProfile:Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogsViewModel>().ReverseMap();
            CreateMap<Blog, AddBlogViewModel>().ReverseMap();
            CreateMap<Blog, BlogDetailViewModel>().ReverseMap();
        }
    }
}
