using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Categories
{
    public class AddCategoryViewModel
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
