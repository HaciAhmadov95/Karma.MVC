using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class CartRepository : ICartService
{
    public Task Create(Cart entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Cart>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, Cart entity)
    {
        throw new NotImplementedException();
    }
}
