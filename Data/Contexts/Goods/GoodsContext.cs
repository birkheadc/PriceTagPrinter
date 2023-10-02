using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Contexts;

public class GoodsContext : DbContext
{
  private readonly IConfiguration configuration;

  public DbSet<Goods> Goods => Set<Goods>();

  public GoodsContext(IConfiguration configuration)
  {
    this.configuration = configuration; 
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite(configuration.GetConnectionString("Goods"));
  }
}