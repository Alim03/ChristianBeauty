using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Interfaces.Banners
{
    public interface IBannerRepositroy  : IRepository<Banner>
    {
        Task<List<Banner>> GetPaginatedBannersByFilterAsync(int pageNumber, int pageSize, int? bannerId);
        Task<List<Banner>> GetPaginatedBannersAsync(int pageNumber, int pageSize);
        Task<Banner> GetBannerById(int bannerId);
        Task<Banner> GetBannerByProductId(int productId);
        Task<int> CountBannersAsync();
        Task<bool> CheckProductHasConfiguredAsBanner(int productId);
    }
}
