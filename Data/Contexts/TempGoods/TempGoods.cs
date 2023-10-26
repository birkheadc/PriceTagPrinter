using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Contexts;

public class TempGoodsContext : DbContext
{
  private readonly string connectionString;

  public DbSet<Goods> Goods => Set<Goods>();

  public TempGoodsContext(string connectionString)
  {
    this.connectionString = connectionString;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite(connectionString);
  }
}