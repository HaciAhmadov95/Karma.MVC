using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class CartProductRepository : ICartProductService
{
	private readonly AppDbContext _context;

	public CartProductRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<CartProduct> Get(int? id)
	{
		CartProduct cartProduct = await _context.CartProduct.Include(n => n.Cart)
															.Include(n => n.Product)
															.ThenInclude(n => n.Images)
															.FirstOrDefaultAsync();
		return cartProduct;
	}

	public async Task<List<CartProduct>> GetAll()
	{
		List<CartProduct> cartProducts = await _context.CartProduct.Include(n => n.Cart)
															.Include(n => n.Product)
															.ThenInclude(n => n.Images)
															.ToListAsync();
		return cartProducts;
	}

	public async Task Create(CartProduct entity)
	{
		await _context.CartProduct.AddAsync(entity);
	}

	public Task Update(int id, CartProduct entity)
	{
		throw new NotImplementedException();
	}

	public Task Delete(int? id)
	{

		throw new NotImplementedException();
	}

	public async Task DeleteProduct(int cartId, int productId)
	{
		CartProduct cartProduct = await _context.CartProduct.Include(n => n.Cart)
							.Include(n => n.Product)
							.Where(n => n.CartId == cartId)
							.Where(n => n.ProductId == productId)
							.FirstOrDefaultAsync();

		_context.CartProduct.Remove(cartProduct);
	}

	public async Task SaveChanges()
	{
		_context.SaveChangesAsync();
	}
}
