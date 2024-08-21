using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Components;

public class FooterViewComponent : ViewComponent
{
	private readonly ISubscriberService _subscriberService;

	public FooterViewComponent(ISubscriberService subscriberService)
	{
		_subscriberService = subscriberService;
	}

	public Task<IViewComponentResult> InvokeAsync()
	{
		Subscriber subscriber = new();

		return Task.FromResult<IViewComponentResult>(View(model: subscriber));
	}
}
