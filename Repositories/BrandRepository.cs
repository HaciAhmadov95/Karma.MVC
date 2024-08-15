using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class BrandRepository : IBrandService
{
    private readonly AppDbContext _context;

    public BrandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Brand> Get(int? id)
    {
        Brand brand = await _context.Brands.Where(n => n.Id == id)
                                           .FirstOrDefaultAsync();

        return brand;
    }

    public async Task<List<Brand>> GetAll()
    {
        List<Brand> brands = await _context.Brands.ToListAsync();

        return brands;
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

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
