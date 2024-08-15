using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class BlogRepository : IBlogService
{
    private readonly AppDbContext _context;

    public BlogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Blog> Get(int? id)
    {
        Blog blog = await _context.Blogs.Where(n => n.Id == id)
                                        .Include(n => n.Images)
                                        .FirstOrDefaultAsync();

        return blog;
    }

    public async Task<List<Blog>> GetAll()
    {
        List<Blog> blogs = await _context.Blogs.Include(n => n.Images)
                                               .ToListAsync();

        return blogs;
    }

    public Task Create(Blog entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Blog entity)
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
