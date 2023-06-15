using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Categories
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
