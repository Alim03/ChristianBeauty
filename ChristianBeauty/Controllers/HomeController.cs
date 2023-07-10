using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChristianBeauty.Models;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Common;
using ChristianBeauty.ViewModels.Products;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Utilities;
using AutoMapper;

namespace ChristianBeauty.Controllers;

public class HomeController : Controller
{
    private protected IProductRepository _productRepository;
    private protected ICategoryRepository _categoryRepository;
    private protected IMapper _mapper;

    private const int RandomProductsNumber = 6;

    public HomeController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper
    )
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetRandomProductsAsync(RandomProductsNumber);
        var viewmodel = _mapper.Map<List<ProductListViewModel>>(products);
        return View(viewmodel);
    }
}
