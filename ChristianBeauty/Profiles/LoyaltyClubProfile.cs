using AutoMapper;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Categories;
using ChristianBeauty.ViewModels.LoyaltyClub;

namespace ChristianBeauty.Profiles
{
    public class LoyaltyClubProfile : Profile
    {
        public LoyaltyClubProfile()
        {
            CreateMap<LoyaltyClubUser, LoyaltyClubListViewModel>();
        }
    }
}
