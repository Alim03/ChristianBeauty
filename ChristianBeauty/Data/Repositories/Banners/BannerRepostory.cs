using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Banners;
using ChristianBeauty.Data.Interfaces.Blogs;
using ChristianBeauty.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristianBeauty.Data.Repositories.Banners
{
    public class BannerRepostory: Repository<Banner>, IBannerRepositroy
    {
        public BannerRepostory(ChristianBeautyDbContext context) : base(context) { }

        public async Task<bool> CheckProductHasConfiguredAsBanner(int productId)
        {
            return await Context.Banners.AnyAsync(x=>x.ProductId == productId);
        }

        public async Task<int> CountBannersAsync()
        {
            return await Context.Banners.CountAsync();
        }

        public async Task<Banner> GetBannerById(int bannerId)
        {
            return await Context.Banners.FirstOrDefaultAsync(x => x.Id == bannerId);

        }

        public async Task<Banner> GetBannerByProductId(int productId)
        {
            return await Context.Banners.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<List<Banner>> GetPaginatedBannersAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Banner> banners = await Context.Banners.Skip(skip).Take(pageSize).ToListAsync();
            return banners;
        }

        public async Task<List<Banner>> GetPaginatedBannersByFilterAsync(int pageNumber, int pageSize, int? bannerId)
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Banner> banners = await Context.Banners.Skip(skip).Take(pageSize).ToListAsync();
            return banners;
        }
    }
}
