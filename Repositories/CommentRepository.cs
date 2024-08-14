using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class CommentRepository : ICommentService
{
    public Task<Comment> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Comment>> GetAll()
    {
        throw new NotImplementedException();
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

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}
