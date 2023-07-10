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
                )
                .ForMember(dest => dest.Gallery, opt => opt.Ignore())
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.SelectedCategoryId)
                );
            ;

            CreateMap<Product, EditProductViewModel>()
                .ForMember(
                    dest => dest.SelectedMaterialId,
                    opt => opt.MapFrom(src => src.MaterialId)
                )
                .ForMember(
                    dest => dest.SelectedCategoryId,
                    opt => opt.MapFrom(src => src.CategoryId)
                )
                .ReverseMap();
            ;
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, GetProductViewModel>().ReverseMap();
            CreateMap<Product, AllProductsViewModel>().ReverseMap();
            CreateMap<Product, ProductListViewModel>()
                .ForMember(
                    dest => dest.Image,
                    opt =>
                        opt.MapFrom(
                            src => src.Gallery.Count > 0 ? src.Gallery.First().ImageName : null
                        )
                );
        }
    }
}
