using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public bool IsFinished { get; set; }

        [MaxLength(64)]
        public string ProductCode { get; set; }
        public int Color { get; set; }

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
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public List<Gallery> Gallery { get; set; }
    }
}
