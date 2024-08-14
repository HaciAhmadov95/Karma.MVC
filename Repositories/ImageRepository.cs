using Karma.MVC.Models;
using Karma.MVC.Services;

namespace Karma.MVC.Repositories;

public class ImageRepository : IImageService
{
    public Task<Image> Get(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Image>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(Image entity)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, Image entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

}
