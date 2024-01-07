using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.Utilities;
using ChristianBeauty.ViewModels.Categories;
using ChristianBeauty.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace ChristianBeauty.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private protected IHttpContextAccessor _httpContextAccessor;


        private readonly IMapper _mapper;

           
        public MenuViewComponent(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var name = HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            UserViewModel userViewModel = new UserViewModel { Name = name };
            return View("Menu", userViewModel);
        }
    }
}
