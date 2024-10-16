using Karma.MVC.Data;
using Karma.MVC.Models.Identity;
using Karma.MVC.Profiles;
using Karma.MVC.Repositories;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
	options.Password.RequiredLength = 8;
	options.User.RequireUniqueEmail = true;
	options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddScoped<IBlogService, BlogRepository>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryRepository>();
builder.Services.AddScoped<IBrandService, BrandRepository>();
builder.Services.AddScoped<ICartService, CartRepository>();
builder.Services.AddScoped<ICategoryService, CategoryRepository>();
builder.Services.AddScoped<ICommentService, CommentRepository>();
builder.Services.AddScoped<IImageService, ImageRepository>();
builder.Services.AddScoped<IProductService, ProductRepository>();
builder.Services.AddScoped<ISubscriberService, SubscriberRepository>();
builder.Services.AddScoped<IWishlistService, WishlistRepository>();
builder.Services.AddScoped<IColorService, ColorRepository>();
builder.Services.AddScoped<ICartProductService, CartProductRepository>();
builder.Services.AddScoped<SettingRepository>();

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
	});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
	name: "default",
	pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
