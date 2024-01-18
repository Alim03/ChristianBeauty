namespace ChristianBeauty.ViewModels.Banners
{
    public class GetBannerWithDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public string Tittle { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
