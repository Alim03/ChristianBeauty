using ChristianBeauty.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChristianBeauty.ViewModels.Products
{
    public class GetProductViewModel
    {


        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public bool IsFinished { get; set; }

        [MaxLength(64)]
        public string ProductCode { get; set; }
        public int? Color { get; set; }

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
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public List<Gallery> Gallery { get; set; }

    }
}
