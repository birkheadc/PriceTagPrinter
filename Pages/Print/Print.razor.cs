using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Pages;
public partial class Print
{
  private GoodsContext? goodsContext;
  private PriceTagContext? priceTagContext;
  public List<PriceTag> PriceTagsToPrint = new();
  public string GoodsCodeToAddToQueue { get; set; } = "";

  protected override async Task OnInitializedAsync()
  {
    goodsContext ??= await GoodsContextFactory.CreateDbContextAsync();

    priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();
    PriceTagsToPrint = await priceTagContext.PriceTags.Where(p => p.NeedsPrinting).ToListAsync();
  }

  public async Task HandleAddToQueue()
  {
    if (goodsContext is null || priceTagContext is null) return;

    Goods? goods = await goodsContext.FindAsync<Goods>(GoodsCodeToAddToQueue);
    if (goods is null)
    {
      GoodsCodeToAddToQueue = "";
      await JsRuntime.InvokeVoidAsync("alert", "That goods does not exist. Please add it to the database, then refresh.");
      return;
    }

    PriceTag? priceTag = priceTagContext.PriceTags.Find(goods.GoodsCode);
    if (priceTag is not null)
    {
      if (priceTag.NeedsPrinting)
      {
        // Todo: Add print-multiple functionality
        GoodsCodeToAddToQueue = "";
        await JsRuntime.InvokeVoidAsync("alert", "That price tag is already scheduled to print!");
        return;
      }
      PriceTagsToPrint.Add(priceTag);
      priceTag.NeedsPrinting = true;
      priceTag.CreatedAt = DateTime.Now;
      await priceTagContext.SaveChangesAsync();
      GoodsCodeToAddToQueue = "";
      return;
    }

    priceTag = PriceTagGoodsConverter.ToPriceTag(goods);
    PriceTagsToPrint.Add(priceTag);
    
    await priceTagContext.PriceTags.AddAsync(priceTag);
    await priceTagContext.SaveChangesAsync();
    GoodsCodeToAddToQueue = "";
  }

  public async Task HandleRemovePriceTag(PriceTag priceTag)
  {
    if (priceTagContext is null) return;

    PriceTagsToPrint.Remove(priceTag);
    priceTag.NeedsPrinting = false;
    priceTagContext.PriceTags.Update(priceTag);
    await priceTagContext.SaveChangesAsync();
  }

  public async Task HandleUpdateDatabase()
  {
    
  }

  public async Task HandlePrintAll()
  {
    await JsRuntime.InvokeVoidAsync("open", $"print-tags", "_blank");
  }

  public async Task HandleClearQueue()
  {
    while (PriceTagsToPrint.Count > 0)
    {
      await HandleRemovePriceTag(PriceTagsToPrint[0]);
    }
  }
}