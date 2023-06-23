using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChristianBeauty.HttpHandler
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserRepository _userRepository;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = context.HttpContext.User.GetUserUsername();
                _userRepository = (IUserRepository)
                    context.HttpContext.RequestServices.GetService(typeof(IUserRepository));
                if (!_userRepository.ChekUserIsAdminByUserName(userName))
                {
                    context.Result = new RedirectResult("/");
                }
            }
        }
    }
}
