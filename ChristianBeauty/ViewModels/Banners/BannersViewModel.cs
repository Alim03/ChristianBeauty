namespace ChristianBeauty.ViewModels.Banners
{
    public class BannersViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnabled { get; set; }
        public int OrderNum { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
