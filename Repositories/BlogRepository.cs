using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class BlogRepository : IBlogService
{
    public Task<Blog> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Blog>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Blog entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Blog entity)
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
