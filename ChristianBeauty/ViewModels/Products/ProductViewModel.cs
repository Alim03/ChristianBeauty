using ChristianBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEnable { get; set; }
        public string? BasalamLink { get; set; }
        public string? DigikalLink { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsTopSeller { get; set; }
        public bool HasBannerConfigured { get; set; }
        public List<Gallery> Gallery { get; set; }
    }
}
