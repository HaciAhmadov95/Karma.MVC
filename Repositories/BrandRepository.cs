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
        Brand brand = await _context.Brands.Where(n => !n.IsDeleted)
                                           .Where(n => n.Id == id)
                                           .Include(n => n.Products)
                                           .FirstOrDefaultAsync();

        return brand;
    }

    public async Task<List<Brand>> GetAll()
    {
        List<Brand> brands = await _context.Brands.Where(n => !n.IsDeleted)
                                                  .Include(n => n.Products)
                                                  .ToListAsync();

        return brands;
    }

    public async Task Create(Brand entity)
    {
        entity.CreateDate = DateTime.UtcNow.AddHours(4);
        await _context.Brands.AddAsync(entity);
    }

    public async Task Update(int id, Brand entity)
    {
        var data = await Get(id);

        data.UpdateDate = DateTime.UtcNow.AddHours(4);
        data.Name = entity.Name;

        _context.Brands.Update(data);
    }

    public async Task Delete(int? id)
    {
        var data = await Get(id);

        data.IsDeleted = true;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
