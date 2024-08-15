using WebApplicationTask.Data.DbContexts;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    private readonly AppDbContext _dbContext = dbContext;

    public ICategoryRepository CategoryRepository => new CategoryRepository(_dbContext);

    public IProductRepository ProductRepository => new ProductRepository(_dbContext);

    public IOrderRepository OrderRepository => new OrderRepository(_dbContext);
}
