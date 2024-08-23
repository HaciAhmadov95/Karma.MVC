using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class ColorRepository : IColorService
{
    private readonly AppDbContext _context;

    public ColorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Color> Get(int? id)
    {
        Color color = await _context.Colors.Where(n => !n.IsDeleted).Where(n => n.Id == id).Include(n => n.Products).FirstOrDefaultAsync();

        return color;
    }

    public async Task<List<Color>> GetAll()
    {
        List<Color> colors = await _context.Colors.Where(n => !n.IsDeleted).Include(n => n.Products).ToListAsync();

        return colors;
    }

    public async Task Create(Color entity)
    {
        entity.CreateDate = DateTime.UtcNow.AddHours(4);
        await _context.Colors.AddAsync(entity);
    }

    public async Task Update(int id, Color entity)
    {
        var data = await Get(id);

        data.UpdateDate = DateTime.UtcNow.AddHours(4);
        data.Name = entity.Name;

        _context.Colors.Update(data);
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
