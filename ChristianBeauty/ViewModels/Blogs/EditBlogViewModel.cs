﻿namespace ChristianBeauty.ViewModels.Blogs
{
    public class EditBlogViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ReadingTime { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
