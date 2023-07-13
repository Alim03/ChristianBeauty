using ChristianBeauty.Models;

namespace ChristianBeauty.ViewModels.Blogs
{
    public class BlogDetailViewModel
    {
        public Blog Blog { get; set; }
        public List<Blog> SimilarBlogs { get; set; }

    }
}
