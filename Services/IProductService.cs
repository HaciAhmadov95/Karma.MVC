using Karma.MVC.Models;
using Karma.MVC.Services.Base;
using Karma.MVC.ViewModels;

namespace Karma.MVC.Services;

public interface IProductService : IBaseService<Product>
{
    Task<List<Product>> GetPagedData(int pageNumber, int pageSize);
    Task<List<GetProductVM>> FilterDataCategory(int filterId);
    Task<List<GetProductVM>> FilterDataBrand(int filterId);
    Task<List<GetProductVM>> FilterDataColor(int filterId);
    Task<List<Product>> SearchProduct(string input);
    Task<int> GetPageCount(int take);
}
