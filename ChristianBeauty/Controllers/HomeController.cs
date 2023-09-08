using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChristianBeauty.Models;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Common;
using ChristianBeauty.ViewModels.Products;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Utilities;
using AutoMapper;
using ChristianBeauty.ViewModels.Home;
using ChristianBeauty.Data.Interfaces.LoyaltyClubUser;

namespace ChristianBeauty.Controllers;

public class HomeController : Controller
{
    private protected IProductRepository _productRepository;
    private protected ILoyaltyClubUserRepository _LoyaltyClubUserRepository;
    private protected ICategoryRepository _categoryRepository;
    private protected IMapper _mapper;

    private const int RandomProductsNumber = 6;
    private const int TopSellerProductsNumber = 6;


    public HomeController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ILoyaltyClubUserRepository loyaltyClubUserRepository
    )
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _LoyaltyClubUserRepository = loyaltyClubUserRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }


    public ActionResult AboutUs()
    {
        return View();
    }

 

    [HttpGet("Home/LoyaltyClub/{mobile}")]
    public async Task<IActionResult> LoyaltyClub(string mobile)
    {
        if (!string.IsNullOrEmpty(mobile))
        {

            if(!Utilites.IsValidMobileNumber(mobile))
            {
                return Json(new { IsSuccessfull = false });
            }
            LoyaltyClubUser loyaltyClubUsers = new LoyaltyClubUser
            {
                Date = DateTime.Now,
                MobileNumber = mobile
            };
            await _LoyaltyClubUserRepository.AddAsync(loyaltyClubUsers);
            await _LoyaltyClubUserRepository.SaveAsync();
            return Json(new { IsSuccessfull = true });
        }
        else
        {
            return Json(new { IsSuccessfull = false });
        }
    }




    [HttpGet("Home/NotFound")]
    public IActionResult NotFound()
    {
        return View();
    }
}
