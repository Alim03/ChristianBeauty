using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.Data.Repositories.Categories;
using ChristianBeauty.Data.Repositories.Materials;
using ChristianBeauty.Utilities;
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
        private readonly INotyfService _toastNotification;



        public HomeController(IUserRepository userRepository, INotyfService toastNotification)
        {
            _userRepository = userRepository;
            _toastNotification = toastNotification;

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
            var id = HttpContext.User.FindFirstValue(ClaimTypes.Surname);

            var user = await _userRepository.GetAsync(Convert.ToInt32(id));
             user.Name = Name == null ? user.Name : Name;
             user.Password = Password == null ? user.Password : Password;
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            return RedirectToAction("SignOut", "Home");
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
