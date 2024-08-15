using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class CommentRepository : ICommentService
{
    private readonly AppDbContext _contex;

    public CommentRepository(AppDbContext contex)
    {
        _contex = contex;
    }

    public async Task<Comment> Get(int? id)
    {
        Comment comment = await _contex.Comments.Where(n => n.Id == id)
                                                .Include(n => n.AppUser)
                                                .ThenInclude(n => n.Image)
                                                .FirstOrDefaultAsync();

        return comment;
    }

    public async Task<List<Comment>> GetAll()
    {
        List<Comment> comments = await _contex.Comments.Include(n => n.AppUser)
                                                       .ThenInclude(n => n.Image)
                                                       .ToListAsync();

        return comments;
    }

    public Task Create(Comment entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, Comment entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChanges()
    {
        await _contex.SaveChangesAsync();
    }
}
