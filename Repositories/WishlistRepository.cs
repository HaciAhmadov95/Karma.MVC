using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class WishlistRepository : IWishlistService
{
	private readonly AppDbContext _context;

	public WishlistRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Wishlist> Get(int? id)
	{
		Wishlist wishlist = await _context.Wishlists.Where(n => !n.IsDeleted)
													.Include(n => n.Products)
													.ThenInclude(n => n.Images)
													.FirstOrDefaultAsync();
		return wishlist;
	}

	public async Task<List<Wishlist>> GetAll()
	{
		List<Wishlist> wishlists = await _context.Wishlists.Where(n => !n.IsDeleted)
														   .Include(n => n.Products)
														   .ThenInclude(n => n.Images)
														   .ToListAsync();

		return wishlists;
	}

	public async Task Create(Wishlist entity)
	{
		entity.CreateDate = DateTime.UtcNow.AddHours(4);

		await _context.Wishlists.AddAsync(entity);
	}

	public async Task Update(int id, Wishlist entity)
	{
		var data = await Get(id);

		if (data is null)
		{
			throw new NullReferenceException();
		}

		data.UpdateDate = DateTime.UtcNow.AddHours(4);
		data.Products = entity.Products;

		_context.Wishlists.Update(data);
	}

	public async Task Delete(int? id)
	{
		var data = await Get(id);

		if (data is null)
		{
			throw new NullReferenceException();
		}

		data.IsDeleted = true;
	}

	public async Task SaveChanges()
	{
		await _context.SaveChangesAsync();
	}
}
