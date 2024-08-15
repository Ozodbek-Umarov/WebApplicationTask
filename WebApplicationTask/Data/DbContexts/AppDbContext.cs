using Microsoft.EntityFrameworkCore;
using WebApplicationTask.Data.Entities;

namespace WebApplicationTask.Data.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}
