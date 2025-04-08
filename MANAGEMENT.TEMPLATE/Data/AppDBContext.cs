using MANAGEMENT.TEMPLATE.Models;
using Microsoft.EntityFrameworkCore;

namespace MANAGEMENT.TEMPLATE.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options) { }

    public DbSet<Model> Models { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Model>().ToTable("table_name", schema: "dbo");
    }
}
