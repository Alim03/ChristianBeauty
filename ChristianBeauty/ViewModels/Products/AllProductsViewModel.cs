using ChristianBeauty.Models;
using System.ComponentModel.DataAnnotations;

namespace ChristianBeauty.ViewModels.Products
{
    public class AllProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public string ProductCode { get; set; }
        public int? Color { get; set; }
        public string? Description { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Gallery> Gallery { get; set; }
    }
}
