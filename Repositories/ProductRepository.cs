using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class ProductRepository : IProductService
{
    public Task<Product> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Product entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Product entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}
