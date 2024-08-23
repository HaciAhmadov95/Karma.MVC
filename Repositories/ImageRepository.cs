using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class ImageRepository : IImageService
{
    private readonly AppDbContext _context;

    public ImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Image> Get(int? id)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var data = await _context.Images.Where(n => n.Id == id).FirstOrDefaultAsync();

        if (data is null)
        {
            throw new NullReferenceException();
        }

        return data;
    }

    public async Task<List<Image>> GetAll()
    {
        var data = await _context.Images.ToListAsync();

        if (data is null)
        {
            throw new NullReferenceException();
        }

        return data;
    }

    public async Task Create(Image entity)
    {
        await _context.Images.AddAsync(entity);
    }

    public async Task Update(int id, Image entity)
    {
        var data = await Get(id);

        if (data is null)
        {
            throw new NullReferenceException();
        }

        data.Url = entity.Url;
    }

    public async Task Delete(int? id)
    {
        var data = await Get(id);

        _context.Images.Remove(data);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
