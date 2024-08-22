using Karma.MVC.Models;
using Karma.MVC.Services.Base;

namespace Karma.MVC.Services;

public interface ICartProductService : IBaseService<CartProduct>
{
	Task DeleteProduct(int cartId, int productId);

}
