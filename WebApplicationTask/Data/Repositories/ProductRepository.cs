using WebApplicationTask.Data.DbContexts;
using WebApplicationTask.Data.Entities;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Data.Repositories;

public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
{
}
