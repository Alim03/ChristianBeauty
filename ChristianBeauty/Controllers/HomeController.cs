using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChristianBeauty.Models;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.ViewModels.Common;
using ChristianBeauty.ViewModels.Products;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Utilities;

namespace ChristianBeauty.Controllers;

public class HomeController : Controller
{
    private protected IProductRepository _productRepository;
    private protected ICategoryRepository _categoryRepository;

    private const int PAGESIZE = 1;

    public HomeController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository
    )
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index(int page = 1, int? categoryId = null)
    {
        List<Product> products;
        int totalCount;
        if (categoryId != null)
        {
            products = await _productRepository.GetPaginatedProductsByCategoryAsync(
                page,
                PAGESIZE,
                categoryId.Value
            );
            totalCount = await _productRepository.GetTotalCountProductsByCategoryAsync(
                categoryId.Value
            );
        }
        else
        {
            products = await _productRepository.GetPaginatedProductsAsync(page, PAGESIZE);
            totalCount = await _productRepository.GetTotalCountProductsAsync();
        }

        var category = await _categoryRepository.GetAllParentCategoriesAsync();
        var categoriesSelectListItem = SelectListHelper.ConvertCategoryToSelectListItems(
            category.ToList()
        );
        PaginationMetadata paginationMetadata = new PaginationMetadata
        {
            TotalCount = totalCount,
            PageSize = PAGESIZE,
            CurrentPage = page
        };
        var viewModel = new PaginatedProductsViewModel
        {
            Products = products,
            Metadata = paginationMetadata,
            Categories = categoriesSelectListItem
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Search(string q, int page = 1)
    {
        var products = await _productRepository.GetPaginatedProductsAsync(page, PAGESIZE, q);
        var totalCount = await _productRepository.GetTotalCountProductsAsync(q);
        PaginationMetadata paginationMetadata = new PaginationMetadata
        {
            TotalCount = totalCount,
            PageSize = PAGESIZE,
            CurrentPage = page
        };
        var viewModel = new PaginatedProductsViewModel
        {
            Products = products,
            Metadata = paginationMetadata,
        };

        return View("Index", viewModel);
    }
}
