using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class WishlistRepository : IWishlistService
{
    public Task<Wishlist> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Wishlist>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Wishlist entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Wishlist entity)
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
