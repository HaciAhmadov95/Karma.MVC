using Karma.MVC.Models;
using Karma.MVC.Services.Base;

namespace Karma.MVC.Services;

public interface IProductService : IBaseService<Product>
{
	Task<List<Product>> GetPagedData(int pageNumber, int pageSize);
}
