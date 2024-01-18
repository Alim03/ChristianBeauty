using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Common;

namespace ChristianBeauty.ViewModels.Categories
{
    public class PaginatedCatrgoryViewModel
    {
        public List<Category> Categories { get; set; }
        public PaginationMetadata Metadata { get; set; }
    }
}
