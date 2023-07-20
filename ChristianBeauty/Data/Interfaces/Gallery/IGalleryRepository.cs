namespace ChristianBeauty.Data.Interfaces.Gallery
{
    public interface IGalleryRepository : IRepository<ChristianBeauty.Models.Gallery>
    {
        Task<List<Models.Gallery>> GetProductImagesByIdAsync(int id);
        Task<Models.Gallery> GetImageByIdAsync(int id);
    }
}
