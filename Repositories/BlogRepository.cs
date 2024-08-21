using Karma.MVC.Data;
using Karma.MVC.Helpers.Extensions;
using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class BlogRepository : IBlogService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IImageService _imageService;
    private readonly UserManager<AppUser> _userManager;

    public BlogRepository(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager, IImageService imageService)
    {
        _context = context;
        _env = env;
        _userManager = userManager;
        _imageService = imageService;
    }

    public async Task<Blog> Get(int? id)
    {
        Blog blog = await _context.Blogs.Where(n => !n.IsDeleted)
                                        .Where(n => n.Id == id)
                                        .Include(n => n.Images)
                                        .Include(n => n.BlogCategory)
                                        .Include(n => n.Comments)
                                        .FirstOrDefaultAsync();

        return blog;
    }

    public async Task<List<Blog>> GetAll()
    {
        List<Blog> blogs = await _context.Blogs.Where(n => !n.IsDeleted)
                                               .Include(n => n.Images)
                                               .Include(n => n.BlogCategory)
                                               .Include(n => n.Comments)
                                               .ToListAsync();

        return blogs;
    }

    public async Task Create(Blog entity)
    {
        List<Image> images = new();

        foreach (var imageFile in entity.ImageFile)
        {
            string fileName = await imageFile.CreateFile(_env);

            Image image = new();
            image.Url = fileName;
            image.IsMain = false;
            images.Add(image);
        }

        Image mainImage = new();
        string mainFileName = await entity.MainFile.CreateFile(_env);
        mainImage.Url = mainFileName;
        mainImage.IsMain = true;
        images.Add(mainImage);

        entity.Images = images;
        entity.BlogCategory = await _context.BlogCategories.Where(n => n.Name == entity.BlogCategory.Name).FirstOrDefaultAsync();

        entity.CreateDate = DateTime.UtcNow.AddHours(4);

        await _context.Blogs.AddAsync(entity);
    }
    public async Task Update(int id, Blog entity)
    {
        List<Image> currentImages = new();
        var data = await Get(id);

        if (entity.ImageFile is not null)
        {
            for (int i = 0; i < data.Images.Where(n => n.IsMain == false).ToList().Count; i++)
            {
                currentImages.Add(data.Images.Where(n => n.IsMain == false).ToList()[i]);
            }

            foreach (var imageFile in entity.ImageFile)
            {
                string fileName = await imageFile.CreateFile(_env);

                Image image = new();
                image.Url = fileName;
                image.IsMain = false;
                currentImages.Add(image);
            }

            var images = data.Images;
            currentImages.AddRange(images);
        }
        else
        {
            for (int i = 0; i < data.Images.Where(n => n.IsMain == false).ToList().Count; i++)
            {
                currentImages.Add(data.Images.Where(n => n.IsMain == false).ToList()[i]);
            }
        }

        if (entity.MainFile is not null)
        {
            string fileName = await entity.MainFile.CreateFile(_env);

            Image image = new();
            image.Url = fileName;
            image.IsMain = true;
            currentImages.Add(image);

            await _imageService.Delete(data.Images.Where(n => n.IsMain == true).FirstOrDefault().Id);
        }
        else
        {
            currentImages.Add(data.Images.Where(n => n.IsMain == true).FirstOrDefault());
        }

        entity.Images = currentImages;

        data.UpdateDate = DateTime.UtcNow.AddHours(4);
        data.Title = entity.Title;
        data.Content = entity.Content;
        data.Images = entity.Images;
        if (entity.Comments != null)
        {
            data.Comments = entity.Comments;
        }

        _context.Blogs.Update(data);
    }

    public async Task Delete(int? id)
    {
        var data = await Get(id);

        data.IsDeleted = true;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

}
