using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class SubscriberRepository : ISubscriberService
{
    public Task<Subscriber> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Subscriber>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Subscriber entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Subscriber entity)
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
