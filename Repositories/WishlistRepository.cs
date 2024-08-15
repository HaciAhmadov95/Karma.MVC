using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class WishlistRepository : IWishlistService
{
    private readonly AppDbContext _context;

    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Wishlist> Get(int? id)
    {
        Wishlist wishlist = await _context.Wishlists.Include(n => n.Products)
                                                    .ThenInclude(n => n.Images)
                                                    .FirstOrDefaultAsync();
        return wishlist;
    }

    public async Task<List<Wishlist>> GetAll()
    {
        List<Wishlist> wishlists = await _context.Wishlists.Include(n => n.Products)
                                                           .ThenInclude(n => n.Images)
                                                           .ToListAsync();

        return wishlists;
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

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
