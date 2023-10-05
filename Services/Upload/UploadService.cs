
using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Services;

public class UploadService : IUploadService
{
  private readonly GoodsContext goodsContext;
  private readonly PriceTagContext priceTagContext;

  public UploadService(IDbContextFactory<GoodsContext> goodsContextFactory, IDbContextFactory<PriceTagContext> priceTagContextFactory)
  {
    goodsContext = goodsContextFactory.CreateDbContext();
    priceTagContext = priceTagContextFactory.CreateDbContext();
  }
  public async Task OverwriteDatabase(IFormFile newDatabase)
  {
    string path = "Data/Databases/Goods/goods.db";
    Console.WriteLine($"Copying file to path: {path}");
    using (Stream stream = new FileStream(path, FileMode.Create))
    {
      await newDatabase.CopyToAsync(stream);
    }
    Console.WriteLine("Finished copying file.");

    await UpdatePriceTags();
  }

  private async Task UpdatePriceTags()
  {
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