using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
