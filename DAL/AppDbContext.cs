using Bizland.Models;
using Microsoft.EntityFrameworkCore;

namespace Bizland.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Team> Teams { get; set; }
}
