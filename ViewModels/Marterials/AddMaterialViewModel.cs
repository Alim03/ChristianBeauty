using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Marterials
{
    public class AddMaterialViewModel
    {
        [Required]
        [MaxLength(64)]
        public string MaterialName { get; set; }
    }
}
