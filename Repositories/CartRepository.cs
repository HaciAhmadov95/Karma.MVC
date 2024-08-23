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

    public async Task Create(Cart entity)
    {
        entity.CreateDate = DateTime.UtcNow.AddHours(4);

        await _context.Carts.AddAsync(entity);
    }

    public async Task Update(int id, Cart entity)
    {
        var data = await Get(id);

        if (data is null)
        {
            throw new NullReferenceException();
        }

        data.UpdateDate = DateTime.UtcNow.AddHours(4);
        data.Products = entity.Products;
        data.TotalPrice = entity.TotalPrice;

        _context.Carts.Update(data);
    }

    public async Task Delete(int? id)
    {
        var data = await Get(id);

        if (data is null)
        {
            throw new NullReferenceException();
        }

        data.IsDeleted = true;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
