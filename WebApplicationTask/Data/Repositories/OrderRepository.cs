using WebApplicationTask.Data.DbContexts;
using WebApplicationTask.Data.Entities;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Data.Repositories;

public class OrderRepository(AppDbContext dbContext) : GenericRepository<Order>(dbContext), IOrderRepository
{
}
