using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class BrandRepository : IBrandService
{
    public Task<Brand> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Brand>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Brand entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, Brand entity)
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
