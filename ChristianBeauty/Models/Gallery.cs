using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChristianBeauty.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
