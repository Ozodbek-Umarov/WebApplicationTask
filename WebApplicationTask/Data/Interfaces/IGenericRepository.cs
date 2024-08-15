using WebApplicationTask.Data.Entities;
namespace WebApplicationTask.Data.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    IQueryable<T> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
