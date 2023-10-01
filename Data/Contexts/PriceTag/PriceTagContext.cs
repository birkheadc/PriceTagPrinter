using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Contexts;

public class PriceTagContext : DbContext
{
  private readonly IConfiguration configuration;

  public DbSet<PriceTag> Goods { get; set; }

  public PriceTagContext(IConfiguration configuration)
  {
    this.configuration = configuration;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite(configuration.GetConnectionString("PriceTag"));
  }
}