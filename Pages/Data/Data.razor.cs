using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Pages;

public partial class Data
{
  private GoodsContext? goodsContext;
  public List<Goods> Goods = new();
  private PriceTagContext? priceTagContext;
  public List<PriceTag> PriceTags = new();

  protected override async Task OnInitializedAsync()
  {
    await Initialize();
  }

  public async Task Initialize()
  {
    goodsContext ??= await GoodsContextFactory.CreateDbContextAsync();
    Goods = await goodsContext.Goods.ToListAsync();

    priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();
    PriceTags = await priceTagContext.PriceTags.ToListAsync();
  }
  // public async Task Test()
  // {
  //   Console.WriteLine("Testing...");

  //   goodsContext ??= await GoodsContextFactory.CreateDbContextAsync(); 
  //   if (goodsContext is null)
  //   {
  //     Console.WriteLine("Goods Context is null...");
  //     return;
  //   }

  //   goods = await goodsContext.Goods.Where(_ => true).FirstAsync();
  //   if (goods is null)
  //   {
  //     Console.WriteLine("Goods Context was not null, but failed to find a goods...");
  //     return;
  //   }
  //   Console.WriteLine($"Found a goods: Code: {goods.GoodsCode} | Name: {goods.GoodsName}");
  // }

  // public async Task ListPriceTags()
  // {
  //   Console.WriteLine("Attempting to list price tags in database...");
  //   priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();

  //   if (priceTagContext is null)
  //   {
  //     Console.WriteLine("But the context was null :(");
  //     return;
  //   }

  //   List<PriceTag> priceTags = await priceTagContext.PriceTags.Where(_ => true).ToListAsync();

  //   Console.WriteLine("Found thise price tags...");
  //   foreach (PriceTag priceTag in priceTags)
  //   {
  //     Console.WriteLine($"Code ({priceTag.GoodsCode}) created at {priceTag.CreatedAt}");
  //   }
  // }

  // public async Task CreateNewPriceTag()
  // {
  //   Console.WriteLine("Attempting to create new price tag...");

  //   PriceTag priceTag = new()
  //   {
  //     GoodsCode = Guid.NewGuid().ToString(),
  //     GoodsName = "Panda",
  //     GoodsPrice = 100,
  //     CreatedAt = DateTime.Now
  //   };

  //   priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();

  //   if (priceTagContext is null)
  //   {
  //     Console.WriteLine("But the context was null :(");
  //     return;
  //   }

  //   priceTagContext.PriceTags.Add(priceTag);
  //   await priceTagContext.SaveChangesAsync();
  // }
}