using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristianBeauty.Data.Repositories.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ChristianBeautyDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllEagerLoadAsync()
        {
            return await Context.Categories.Include(c => c.Subcategories).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllParentCategoriesEagerLoadAsync()
        {
            return await Context.Categories
                .Where(x => x.ParentCategoryId == null)
                .Include(c => c.Subcategories)
                .ToListAsync();
        }
    }
}
