using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.LoyaltyClubUser;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Repositories.LoyaltyClubUser
{
    public class LoyaltyClubUserRepository : Repository<Models.LoyaltyClubUser> , ILoyaltyClubUserRepository
    {
        public LoyaltyClubUserRepository(ChristianBeautyDbContext context) : base(context)
        {
        }

    }
}
