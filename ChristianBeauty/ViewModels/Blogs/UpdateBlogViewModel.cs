namespace ChristianBeauty.ViewModels.Blogs
{
    public class UpdateBlogViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Tittle { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ReadingTime { get; set; }
        public string Labels { get; set; }
    }
}
