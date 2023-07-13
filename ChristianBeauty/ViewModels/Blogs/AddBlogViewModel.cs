namespace ChristianBeauty.ViewModels.Blogs
{
    public class AddBlogViewModel
    {
        public string Tittle { get; set; }
        public string Description { get; set; }
        public int ReadingTime { get; set; }

        public IFormFile Image { get; set; }
    }
}
