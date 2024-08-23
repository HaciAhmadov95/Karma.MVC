using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class CategoryRepository : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category> Get(int? id)
    {
        Category category = await _context.Categories.Where(n => !n.IsDeleted)
                                                     .Include(n => n.Products)
                                                     .Where(n => n.Id == id)
                                                     .FirstOrDefaultAsync();
        return category;
    }

    public async Task<List<Category>> GetAll()
    {
        List<Category> categories = await _context.Categories.Where(n => !n.IsDeleted)
                                                             .Include(n => n.Products)
                                                             .ToListAsync();

        return categories;
    }

    public async Task Create(Category entity)
    {
        entity.CreateDate = DateTime.UtcNow.AddHours(4);
        await _context.Categories.AddAsync(entity);
    }

    public async Task Update(int id, Category entity)
    {
        var data = await Get(id);

        data.UpdateDate = DateTime.UtcNow.AddHours(4);
        data.Name = entity.Name;

        _context.Categories.Update(data);
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
