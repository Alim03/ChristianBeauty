using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Blogs;
using ChristianBeauty.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChristianBeauty.Data.Repositories.Blogs
{
    public class BlogsRepositry : Repository<Blog>, IBlogsRepository
    {
        public BlogsRepositry(ChristianBeautyDbContext context) : base(context)
        {
        }

        public async Task<Blog> GetBlogById(int blogId)
        {
            return await Context.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);
        }

        public async Task<List<Blog>> GetPaginatedBlogsByFilterAsync(int pageNumber, int pageSize, int? blogId)
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Blog> products = await Context.Blogs
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return products;
        }
        public async Task<int> CountBlogsAsync()
        {
            return await Context.Blogs.CountAsync();
        }


        public async Task<List<Blog>> GetPaginatedBlogssAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            List<Blog> products = await Context.Blogs
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return products;
        }
    }
}
