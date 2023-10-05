using Microsoft.EntityFrameworkCore;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Pages;

public partial class Data
{
  public List<Goods> Goods = new();
  public List<PriceTag> PriceTags = new();

  protected override async Task OnInitializedAsync()
  {
    await Initialize();
  }

  public async Task Initialize()
  {
    using GoodsContext goodsContext = GoodsContextFactory.CreateDbContext();
    using PriceTagContext priceTagContext = PriceTagContextFactory.CreateDbContext();
    Goods = await goodsContext.Goods.ToListAsync();
    PriceTags = await priceTagContext.PriceTags.ToListAsync();
  }
}