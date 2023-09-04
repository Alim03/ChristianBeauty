using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Data.Repositories.Categories;
using ChristianBeauty.Data.Repositories.Materials;
using ChristianBeauty.ViewModels.Products;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
