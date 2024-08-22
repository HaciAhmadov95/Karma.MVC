using AutoMapper;
using Karma.MVC.Data;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class CartRepository : ICartService
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public CartRepository(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Cart> Get(int? id)
	{
		Cart cart = await _context.Carts.Where(n => !n.IsDeleted)
										.Include(n => n.CartProduct)
										.ThenInclude(n => n.Product)
										.ThenInclude(n => n.Images)
										.FirstOrDefaultAsync(n => n.Id == id);

		cart.TotalPrice = 0;

		foreach (var cartProduct in cart.CartProduct)
		{
			if (cartProduct.Product.DiscountValue != 0)
			{
				cart.TotalPrice += (double)((cartProduct.Product.Price - (cartProduct.Product.Price * cartProduct.Product.DiscountValue / 100)) * cartProduct.Quantity);
			}
			else
			{
				cart.TotalPrice += cartProduct.Product.Price * cartProduct.Quantity;
			}
		}

		return cart;
	}

	public async Task<List<Cart>> GetAll()
	{
		List<Cart> carts = await _context.Carts.Where(n => !n.IsDeleted)
											   .Include(n => n.CartProduct)
											   .ThenInclude(n => n.Product)
											   .ToListAsync();


		return carts;
	}

	public async Task Create(Cart entity)
	{
		entity.CreateDate = DateTime.UtcNow.AddHours(4);

		await _context.Carts.AddAsync(entity);
	}

	public async Task Update(int id, Cart entity)
	{
		var data = await Get(id);

		if (data is null)
		{
			throw new NullReferenceException();
		}

		data.UpdateDate = DateTime.UtcNow.AddHours(4);
		data.TotalPrice = entity.TotalPrice;

		_context.Carts.Update(data);
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

	public async Task<List<CartProductVM>> ChangeQuantity(int quantity, int productId, int cartId)
	{
		CartProduct cartProduct = await _context.CartProduct.Where(n => n.CartId == cartId)
															.Where(n => n.ProductId == productId)
															.Include(n => n.Product)
															.ThenInclude(n => n.Images)
															.FirstOrDefaultAsync();

		cartProduct.Quantity = quantity;
		_context.CartProduct.Update(cartProduct);
		await SaveChanges();
		List<CartProduct> cartProducts = await _context.CartProduct.Where(n => n.CartId == cartId)
															.Include(n => n.Product)
															.ThenInclude(n => n.Images)
															.ToListAsync();

		List<CartProductVM> cartProductVMs = _mapper.Map<List<CartProductVM>>(cartProducts);

		cartProductVMs.ForEach(n => n.TotalPrice = cartProducts.Where(n => n.Product.DiscountValue != 0).Select(n => (double)(n.Product.Price - (n.Product.Price * n.Product.DiscountValue / 100)) * n.Quantity).Sum());
		cartProductVMs.ForEach(n => n.TotalPrice += cartProducts.Where(n => n.Product.DiscountValue == 0).Select(n => n.Product.Price * n.Quantity).Sum());

		return cartProductVMs;
	}

	public async Task SaveChanges()
	{
		await _context.SaveChangesAsync();
	}
}
