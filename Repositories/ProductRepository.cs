using AutoMapper;
using Karma.MVC.Data;
using Karma.MVC.Helpers.Extensions;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Repositories;

public class ProductRepository : IProductService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
    }

    public async Task<Product> Get(int? id)
    {
        Product product = await _context.Products.Where(n => !n.IsDeleted)
                                                 .Where(n => n.Id == id)
                                                 .Include(n => n.Brand)
                                                 .Include(n => n.Category)
                                                 .Include(n => n.Colors)
                                                 .Include(n => n.Comments)
                                                 .ThenInclude(n => n.AppUser)
                                                 .Include(n => n.Images)
                                                 .FirstOrDefaultAsync();

        return product;
    }

    public async Task<List<Product>> GetAll()
    {
        List<Product> products = await _context.Products.Where(n => !n.IsDeleted)
                                                        .Include(n => n.Brand)
                                                        .Include(n => n.Images)
                                                        .ToListAsync();

        return products;
    }

    public async Task Create(Product entity)
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
        string mainFileName = await entity.MainImage.CreateFile(_env);
        mainImage.Url = mainFileName;
        mainImage.IsMain = true;
        images.Add(mainImage);

        entity.Images = images;

        await _context.Products.AddAsync(entity);
    }
    public async Task Update(int id, Product entity)
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

        if (entity.MainImage is not null)
        {
            string fileName = await entity.MainImage.CreateFile(_env);

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

        _context.Products.Update(entity);
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

    public async Task<List<Product>> GetPagedData(int pageNumber = 1, int pageSize = 6)
    {

        List<Product> data = await _context.Products.Where(n => !n.IsDeleted).Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .Include(n => n.Brand)
                                          .Include(n => n.Images)
                                          .ToListAsync();

        return data;
    }

    public async Task<List<GetProductVM>> FilterDataCategory(int filterId)
    {
        List<Product> data = await _context.Products.Where(n => !n.IsDeleted)
                                        .Include(n => n.Category)
                                        .Where(n => n.Category.Id == filterId)
                                        .Include(n => n.Brand)
                                        .Include(n => n.Images)
                                        .ToListAsync();

        List<GetProductVM> getProductVM = _mapper.Map<List<GetProductVM>>(data);

        return getProductVM;
    }

    public async Task<List<GetProductVM>> FilterDataBrand(int filterId)
    {
        List<Product> data = await _context.Products.Where(n => !n.IsDeleted)
                                        .Include(n => n.Brand)
                                        .Where(n => n.Brand.Id == filterId)
                                        .Include(n => n.Images)
                                        .ToListAsync();

        List<GetProductVM> getProductVM = _mapper.Map<List<GetProductVM>>(data);

        return getProductVM;
    }

    public async Task<List<GetProductVM>> FilterDataColor(int filterId)
    {
        List<Product> data = await _context.Products.Where(n => !n.IsDeleted)
                                        .Include(n => n.Brand)
                                        .Include(n => n.Colors)
                                        .Where(n => n.Colors.Any(c => c.Id == filterId))
                                        .Include(n => n.Images)
                                        .ToListAsync();

        List<GetProductVM> getProductVM = _mapper.Map<List<GetProductVM>>(data);

        return getProductVM;
    }

    public async Task<List<Product>> SearchProduct(string input)
    {
        List<Product> data = await _context.Products.Where(n => !n.IsDeleted)
                                        .Include(n => n.Category)
                                        .Where(n => !n.IsDeleted)
                                        .Where(n => n.Title.Contains(input))
                                        .Include(n => n.Brand)
                                        .Include(n => n.Images)
                                        .ToListAsync();

        //List<GetProductVM> getProductVM = _mapper.Map<List<GetProductVM>>(data);

        return data;
    }

    public async Task<int> GetPageCount(int take)
    {
        IEnumerable<Product> products = await GetAll();

        return (int)Math.Ceiling((decimal)products.Count() / take);
    }
}
