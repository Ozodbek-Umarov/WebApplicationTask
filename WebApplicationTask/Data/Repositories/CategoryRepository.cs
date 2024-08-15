using WebApplicationTask.Data.DbContexts;
using WebApplicationTask.Data.Entities;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Data.Repositories;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
{
}
