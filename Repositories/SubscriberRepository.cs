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
        Subscriber subscriber = await _context.Subscribers.Where(n => !n.IsDeleted)
                                                          .Where(n => n.Id == id)
                                                          .FirstOrDefaultAsync();

        return subscriber;
    }

    public async Task<List<Subscriber>> GetAll()
    {
        List<Subscriber> subscribers = await _context.Subscribers.Where(n => !n.IsDeleted)
                                                                 .ToListAsync();

        return subscribers;
    }

    public async Task Create(Subscriber entity)
    {
        await _context.Subscribers.AddAsync(entity);
    }

    public async Task Update(int id, Subscriber entity)
    {
        var data = await Get(id);

        data.SubscriberEmail = entity.SubscriberEmail;
        _context.Subscribers.Update(data);
    }

    public async Task Delete(int? id)
    {
        var data = await Get(id);

        _context.Subscribers.Remove(data);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
