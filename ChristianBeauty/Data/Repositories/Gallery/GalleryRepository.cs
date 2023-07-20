using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Gallery;
using Microsoft.EntityFrameworkCore;

namespace ChristianBeauty.Data.Repositories.Gallery
{
    public class GalleryRepository : Repository<ChristianBeauty.Models.Gallery>, IGalleryRepository
    {
        public GalleryRepository(ChristianBeautyDbContext context) : base(context) { }

        public async Task<List<Models.Gallery>> GetProductImagesByIdAsync(int id)
        {
            return await Context.Galleries.Where(x => x.ProductId == id).ToListAsync();
        }

        public async Task<Models.Gallery> GetImageByIdAsync(int id)
        {
            return await Context.Galleries.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
