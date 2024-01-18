namespace ChristianBeauty.ViewModels.Blogs
{
    public class BlogsViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ReadingTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string Labels { get; set; }
    }
}
