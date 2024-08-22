using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class BlogCategoryRepository : IBlogCategoryService
{
	private readonly AppDbContext _context;

	public BlogCategoryRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<BlogCategory> Get(int? id)
	{
		BlogCategory blogCategory = await _context.BlogCategories.Where(n => !n.IsDeleted)
																 .Where(n => n.Id == id)
																 .FirstOrDefaultAsync();

		return blogCategory;
	}

	public async Task<List<BlogCategory>> GetAll()
	{
		List<BlogCategory> blogCategories = await _context.BlogCategories.Include(m => m.Blogs).Where(n => !n.IsDeleted)
																		 .ToListAsync();

		return blogCategories;
	}

	public async Task Create(BlogCategory entity)
	{
		await _context.BlogCategories.AddAsync(entity);
	}

	public async Task Update(int id, BlogCategory entity)
	{
		var data = await Get(id);

		data.Name = entity.Name;

		_context.BlogCategories.Update(data);
	}

	public async Task Delete(int? id)
	{
		var data = await Get(id);

		_context.BlogCategories.Remove(data);
	}

	public async Task SaveChanges()
	{
		await _context.SaveChangesAsync();
	}
}
