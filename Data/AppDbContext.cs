using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
