namespace ChristianBeauty.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public  int ProductId { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
