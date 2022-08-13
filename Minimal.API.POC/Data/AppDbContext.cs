using Microsoft.EntityFrameworkCore;
using Minimal.API.POC.Model;

namespace Minimal.API.POC.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person>? People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=minimal.db;Cache=Shared");
}
