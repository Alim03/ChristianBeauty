using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Models;
using ChristianBeauty.ViewModels.Categories;

namespace ChristianBeauty.Data.Interfaces.Categories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllParentCategoriesEagerLoadAsync(int pageIdint, int pageSize);
        Task<GetCategoryViewModel> GetCategoryAndSubNameAsync(int id);
        Task<IEnumerable<Category>> GetAllParentCategoriesAsync();
        IEnumerable<Category> GetAllParentsSubCategories(int id);
        Task<int> GetAllCategoriesCount();

        Task<int> GetSubCategoryParentAsync(int id);

        Task<IEnumerable<Category>> GetAllEagerLoadAsync();
        Task<bool> IsSubCategoryAsync(int id);
    }
}
