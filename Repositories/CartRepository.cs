using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class CartRepository : ICartService
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> Get(int? id)
    {
        Cart cart = await _context.Carts.Where(n => !n.IsDeleted)
                                        .Include(n => n.Products)
                                        .ThenInclude(n => n.Images)
                                        .FirstOrDefaultAsync();

        return cart;
    }

    public async Task<List<Cart>> GetAll()
    {
        List<Cart> carts = await _context.Carts.Where(n => !n.IsDeleted)
                                               .Include(n => n.Products)
                                               .ThenInclude(n => n.Images)
                                               .ToListAsync();
        return carts;
    }

    public Task Create(Cart entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, Cart entity)
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
