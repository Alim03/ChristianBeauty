using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.Models;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private protected IUserRepository _userRepository;
        private readonly INotyfService _toastNotification;

        public UserController(IUserRepository userRepository, INotyfService toastNotification)
        {
            _userRepository = userRepository;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserByUsername(viewModel.Username);
                if (user == null)
                {
                    _toastNotification.Error(ErrorsMessages.WrongUsernameOrPassword, 10);
                    return View("Login");
                }
                if (user.Password != viewModel.Password)
                {
                    _toastNotification.Error(ErrorsMessages.WrongUsernameOrPassword, 10);
                    return View("Login");
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                    new Claim(ClaimTypes.GivenName, user.Name.ToString()),
                    new Claim(ClaimTypes.Surname, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties { IsPersistent = true };
                HttpContext.SignInAsync(principal, properties);
                _toastNotification.Success(ErrorsMessages.Success, 10);

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(viewModel);
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
