using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Products;

namespace ChristianBeauty.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductViewModel, Product>()
                .ForMember(
                    dest => dest.MaterialId,
                    opt => opt.MapFrom(src => src.SelectedMaterialId)
                );
            CreateMap<Product, EditProductViewModel>()
                .ForMember(
                    dest => dest.SelectedMaterialId,
                    opt => opt.MapFrom(src => src.MaterialId)
                )
                .ReverseMap();
            ;
            CreateMap<Product, ProductViewModel>();
        }
    }
}
