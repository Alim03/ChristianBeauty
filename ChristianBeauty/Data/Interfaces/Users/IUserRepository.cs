using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Interfaces.Users
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByUsername(string username);
        bool ChekUserIsAdminByUserName(string username);
    }
}
