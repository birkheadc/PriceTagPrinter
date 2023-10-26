
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Services;

public class UploadService : IUploadService
{
  private readonly IDbContextFactory<GoodsContext> goodsContextFactory;
  private readonly IDbContextFactory<PriceTagContext> priceTagContextFactory;
  private const string DATABASE_PATH = "Data/Databases/Goods/goods.db";

  public UploadService(IDbContextFactory<GoodsContext> goodsContextFactory, IDbContextFactory<PriceTagContext> priceTagContextFactory)
  {
    this.goodsContextFactory = goodsContextFactory;
    this.priceTagContextFactory = priceTagContextFactory;
  }

  public async Task OverwriteDatabase(IFormFile newDatabase)
  {
    Console.WriteLine($"Copying file to path: {DATABASE_PATH}");
    using (Stream stream = new FileStream(DATABASE_PATH, FileMode.Create))
    {
      await newDatabase.CopyToAsync(stream);
    }
    Console.WriteLine("Finished copying file.");

    await UpdatePriceTags();
  }

  private async Task UpdatePriceTags()
  {
    using GoodsContext goodsContext = goodsContextFactory.CreateDbContext();
    using PriceTagContext priceTagContext = priceTagContextFactory.CreateDbContext();

    List<PriceTag> priceTags = priceTagContext.PriceTags.ToList();

    foreach (PriceTag priceTag in priceTags)
    {
      Goods? goods = await goodsContext.Goods.FindAsync(priceTag.GoodsCode);
      if (goods is null)
      {
        // If the item is no longer in the database, remove it from pricetags as well
        priceTagContext.Remove(priceTag);
        continue;
      }
      if (priceTag.NeedsPrinting == true)
      {
        // If the price tag is already marked as needing to print, leave it that way
        continue;
      }
      if (priceTag.GoodsName == goods.GoodsName && priceTag.GoodsPrice == goods.GoodsCost)
      {
        // If the price tag's name and price are still the same as the database, it does not need to be printed
        continue;
      }
      // At this point, the price tag's details are no longer up-to-date
      // Make them up to date, schedule the price tag for re-printing, then save the context
      ModifyPriceTagToMatchGoods(priceTag, goods);
      priceTag.NeedsPrinting = true;
      priceTagContext.Update(priceTag);
    }
    await priceTagContext.SaveChangesAsync();
  }

  private void ModifyPriceTagToMatchGoods(PriceTag priceTag, Goods goods)
  {
    if (priceTag.GoodsCode != goods.GoodsCode)
    {
      Console.WriteLine("These two items are not the same! Aborting.");
      return;
    }
    priceTag.GoodsName = goods.GoodsName;
    priceTag.GoodsPrice = goods.GoodsCost;
  }
}