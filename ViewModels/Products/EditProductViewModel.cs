using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristianBeauty.ViewModels.Products
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public bool IsFinished { get; set; }

        [MaxLength(64)]
        public string ProductCode { get; set; }

        [MaxLength(512)]
        public string? Description { get; set; }
        public int? Size { get; set; }
        public decimal? Weight { get; set; }

        [MaxLength(512)]
        public string? Features { get; set; }

        [MaxLength(512)]
        public string? OtherDetails { get; set; }

        [MaxLength(256)]
        public string? DigikalLink { get; set; }

        [MaxLength(256)]
        public string? BasalamLink { get; set; }
        public int SelectedMaterialId { get; set; }
        public List<SelectListItem>? Materials { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public int? SelectedSubCategoryId { get; set; }
        public List<SelectListItem>? SubCategories { get; set; }
    }
}
