using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChristianBeauty.HttpHandler
{
    public static class ClaimsExtentions
    {
        // public static int GetUserId(this ClaimsPrincipal principal)
        // {
        //     var claimValue = principal.Claims.SingleOrDefault(
        //         item => item.Type == ClaimTypes.NameIdentifier
        //     );
        //     if (claimValue != null && claimValue.Value != null)
        //     {
        //         return Convert.ToInt32(claimValue.Value);
        //     }

        //     return default;
        // }

        public static string GetUserUsername(this ClaimsPrincipal principal)
        {
            var claimValue = principal.Claims.SingleOrDefault(
                item => item.Type == ClaimTypes.NameIdentifier
            );
            if (claimValue != null && claimValue.Value != null)
            {
                return claimValue.Value;
            }

            return default;
        }
    }
}
