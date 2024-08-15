using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class ProductRepository : IProductService
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Get(int? id)
    {
        Product product = await _context.Products.Where(n => n.Id == id)
                                                 .Include(n => n.Brand)
                                                 .Include(n => n.Category)
                                                 .Include(n => n.Colors)
                                                 .Include(n => n.Comments)
                                                 .ThenInclude(n => n.AppUser)
                                                 .Include(n => n.Images)
                                                 .FirstOrDefaultAsync();

        return product;
    }

    public async Task<List<Product>> GetAll()
    {
        List<Product> products = await _context.Products.ToListAsync();

        return products;
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

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
