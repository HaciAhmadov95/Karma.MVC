using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class CommentRepository : ICommentService
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> Get(int? id)
    {
        Comment comment = await _context.Comments.Where(n => !n.IsDeleted)
                                                .Where(n => n.Id == id)
                                                .Include(n => n.AppUser)
                                                .ThenInclude(n => n.Image)
                                                .FirstOrDefaultAsync();

        return comment;
    }

    public async Task<List<Comment>> GetAll()
    {
        List<Comment> comments = await _context.Comments.Where(n => !n.IsDeleted)
                                                       .Include(n => n.AppUser)
                                                       .ThenInclude(n => n.Image)
                                                       .ToListAsync();

        return comments;
    }

    public async Task Create(Comment entity)
    {
        entity.CreateDate = DateTime.UtcNow.AddHours(4);

        await _context.Comments.AddAsync(entity);
    }

    public async Task Update(int id, Comment entity)
    {
        var data = await Get(id);

        data.UpdateDate = DateTime.UtcNow.AddHours(4);
        data.Content = entity.Content;
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
