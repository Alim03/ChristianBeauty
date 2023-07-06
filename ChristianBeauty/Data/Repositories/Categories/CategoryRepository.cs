using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Categories;
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
        public async Task<GetCategoryViewModel> GetCategoryAndSubNameAsync(int id)
        {
            var subCategory = await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);
         
            var father =  await Context.Categories.SingleOrDefaultAsync(x => x.Id == subCategory.ParentCategoryId);
            GetCategoryViewModel category = new GetCategoryViewModel
            {
                CategoryName = father.Name,
                SubcategoryName = subCategory.Name
            };
            return category;


        }

        public async Task<IEnumerable<Category>> GetAllParentCategoriesAsync()
        {
            return await Context.Categories.Where(x => x.ParentCategoryId == null).ToListAsync();
        }

        public IEnumerable<Category> GetAllParentsSubCategories(int id)
        {
            return Context.Categories
                .Where(x => x.ParentCategoryId == id)
                .Select(x => new Category { Id = x.Id, Name = x.Name, })
                .ToList();
        }

        public async Task<bool> IsSubCategoryAsync(int id)
        {
            var category = await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            return category.ParentCategoryId != null;
        }

        public async Task<int> GetSubCategoryParentAsync(int id)
        {
            var category = await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            return category.ParentCategoryId.Value;
        }

      
    }
}
