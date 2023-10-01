using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Pages;

public partial class Data
{
  private GoodsContext? goodsContext;
  public Goods? goods { get; set; }

  private PriceTagContext? priceTagContext;
  public DbSet<PriceTag> priceTags { get; set; }
  public async Task Test()
  {
    Console.WriteLine("Testing...");

    goodsContext ??= await GoodsContextFactory.CreateDbContextAsync(); 
    if (goodsContext is null)
    {
      Console.WriteLine("Goods Context is null...");
      return;
    }

    goods = await goodsContext.Goods.Where(_ => true).FirstAsync();
    if (goods is null)
    {
      Console.WriteLine("Goods Context was not null, but failed to find a goods...");
      return;
    }
    Console.WriteLine($"Found a goods: Code: {goods.GoodsCode} | Name: {goods.GoodsName}");
  }

  public async Task ListPriceTags()
  {
    Console.WriteLine("Attempting to list price tags in database...");
    priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();

    if (priceTagContext is null)
    {
      Console.WriteLine("But the context was null :(");
      return;
    }


  }

  public async Task CreateNewPriceTag()
  {
    Console.WriteLine("Attempting to create new price tag...");

    PriceTag priceTag = new()
    {
      GoodsCode = "000",
      GoodsName = "Panda",
      GoodsPrice = 100,
      CreatedAt = DateTime.Now
    };

    priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();

    if (priceTagContext is null)
    {
      Console.WriteLine("But the context was null :(");
      return;
    }

    await priceTagContext.AddAsync(priceTag);
  }
}