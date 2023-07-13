using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Interfaces.Blogs
{
    public interface IBlogsRepository : IRepository<Blog>
    {
        Task<List<Blog>> GetPaginatedBlogsByFilterAsync(int pageNumber, int pageSize, int? blogId);
        Task<List<Blog>> GetPaginatedBlogssAsync(int pageNumber, int pageSize);
        Task<Blog> GetBlogById(int blogId);
        Task<int> CountBlogsAsync();
        Task<List<Blog>> GetRandomBlogAsync(int number);
        Task<List<Blog>> GetRandomBlogAsync(int number, int excludedBlogId);
    }
}
