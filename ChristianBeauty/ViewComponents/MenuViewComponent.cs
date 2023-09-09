using AutoMapper;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.ViewModels.Categories;
using ChristianBeauty.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MenuViewComponent(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userRepository.GetAsync(2);
            UserViewModel userViewModel = new UserViewModel { Name = user.Name };
            return View("Menu", userViewModel);
        }
    }
}
