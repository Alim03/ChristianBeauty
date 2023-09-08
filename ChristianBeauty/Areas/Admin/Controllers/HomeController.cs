using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.Data.Repositories.Categories;
using ChristianBeauty.Data.Repositories.Materials;
using ChristianBeauty.ViewModels.Products;
using ChristianBeauty.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(string Name,string Password)
        {
            var user = await _userRepository.GetAsync(1);
             user.Name = Name == null ? user.Name : Name;
             user.Password = Password == null ? user.Password : Password;
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
