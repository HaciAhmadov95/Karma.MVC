using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class SubscriberRepository : ISubscriberService
{
    private readonly AppDbContext _context;

    public SubscriberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Subscriber> Get(int? id)
    {
        Subscriber subscriber = await _context.Subscribers.Where(n => n.Id == id)
                                                          .FirstOrDefaultAsync();

        return subscriber;
    }

    public async Task<List<Subscriber>> GetAll()
    {
        List<Subscriber> subscribers = await _context.Subscribers.ToListAsync();

        return subscribers;
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

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
