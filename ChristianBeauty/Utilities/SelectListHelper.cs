using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristianBeauty.Utilities
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> ConvertMaterialToSelectListItems(
            List<Material> materials
        )
        {
            return materials
                .Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name })
                .ToList();
        }

        public static List<SelectListItem> ConvertCategoryToSelectListItems(
            List<Category> categories
        )
        {
            return categories
                .Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name })
                .ToList();
        }
    }
}
