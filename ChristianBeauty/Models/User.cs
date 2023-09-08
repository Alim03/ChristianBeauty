using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string UserName { get; set; }
        public string Name { get; set; }

        [MaxLength(128)]
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
