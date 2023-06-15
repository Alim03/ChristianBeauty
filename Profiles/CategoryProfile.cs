using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Categories;

namespace ChristianBeauty.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
