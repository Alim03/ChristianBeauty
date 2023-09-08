using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChristianBeautyDbContext context) : base(context) { }

        public User? GetUserByUsername(string username)
        {
            return Context.Users.SingleOrDefault(x => x.UserName == username);
        }

        public bool ChekUserIsAdminByUserName(string username)
        {
            var user = Context.Users.SingleOrDefault(x => x.UserName == username);
            var f = user;
            if (user?.UserRoleId == 1) //id 1 is admin
            {
                return true;
            }
            return false;
        }
    }
}
