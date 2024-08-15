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
        Category category = await _context.Categories.Where(n => n.Id == id)
                                                     .FirstOrDefaultAsync();
        return category;
    }

    public async Task<List<Category>> GetAll()
    {
        List<Category> categories = await _context.Categories.ToListAsync();

        return categories;
    }

    public Task Create(Category entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Category entity)
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
