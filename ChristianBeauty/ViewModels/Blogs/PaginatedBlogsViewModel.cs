using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristianBeauty.ViewModels.Blogs
{
    public class PaginatedBlogsViewModel
    {
            public List<Blog> Blogs { get; set; }
            public PaginationMetadata Metadata { get; set; }
    }
}
