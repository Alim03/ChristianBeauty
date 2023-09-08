using AutoMapper;
using ChristianBeauty.Data.Interfaces.Blogs;
using ChristianBeauty.Data.Interfaces.LoyaltyClubUser;
using ChristianBeauty.ViewModels.Blogs;
using ChristianBeauty.ViewModels.LoyaltyClub;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class LoyaltyClubUserController : AdminBaseController
    {
        private protected ILoyaltyClubUserRepository _repository;
        private readonly IMapper _mapper;


        public LoyaltyClubUserController(ILoyaltyClubUserRepository loyaltyClubUserRepository, IMapper mapper)
        {
            _repository = loyaltyClubUserRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAllAsync();
            var viewModel = _mapper.Map<List<LoyaltyClubListViewModel>>(result);
            return View(viewModel);
        }
    }
}
