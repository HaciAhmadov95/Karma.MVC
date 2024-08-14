using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class CategoryRepository : ICategoryService
{
    public Task<Category> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Category entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Category entity)
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
