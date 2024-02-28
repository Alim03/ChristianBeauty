using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristianBeauty.ViewModels.Products
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage ="وارد کردن نام محصول الزامیست")]
        [MaxLength(64)]
        public string Name { get; set; }
        public bool IsAvailable { get; set; } = true ;
        public bool IsTopSeller { get; set; } 


        [MaxLength(64)]
        [Required(ErrorMessage = "وارد کردن کد محصول الزامیست")]

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
        [Required(ErrorMessage = "وارد کردن جنس محصول الزامیست")]

        public int SelectedMaterialId { get; set; }

        public List<SelectListItem>? Materials { get; set; }
        [Required(ErrorMessage = "وارد کردن دسته بندی محصول الزامیست")]

        public int SelectedCategoryId { get; set; }


        public List<SelectListItem>? Categories { get; set; }


        public int? SelectedSubCategoryId { get; set; }

        public List<SelectListItem>? SubCategories { get; set; }

        [Required(ErrorMessage = "وارد کردن  تصویر محصول الزامیست")]
        public IFormFileCollection Gallery { get; set; }
    }
}
