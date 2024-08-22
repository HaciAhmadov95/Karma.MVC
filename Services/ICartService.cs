using Karma.MVC.Models;
using Karma.MVC.Services.Base;
using Karma.MVC.ViewModels;

namespace Karma.MVC.Services;

public interface ICartService : IBaseService<Cart>
{
    Task<List<CartProductVM>> ChangeQuantity(int quantity, int productId, int cartId);
}
