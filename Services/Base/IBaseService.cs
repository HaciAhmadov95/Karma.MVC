namespace Karma.MVC.Services.Base;

public interface IBaseService<TEntity>
    where TEntity : class, new()
{
    Task<TEntity> Get(int? id);
    Task<List<TEntity>> GetAll();
    Task Create(TEntity entity);
    Task Update(int id, TEntity entity);
    Task Delete(int? id);
    Task SaveChanges();

}
