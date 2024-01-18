using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Users
{
    public class LoginViewModel
    {
        [MaxLength(64)]
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        public string Username { get; set; }

        [MaxLength(128)]
        [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید")]
        public string Password { get; set; }
    }
}
