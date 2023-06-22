using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Marterials;

namespace ChristianBeauty.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, MaterialViewModel>();
        }
    }
}
