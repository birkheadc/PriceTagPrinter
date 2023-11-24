using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Pages;
public partial class Print
{
  public List<PriceTag> PriceTagsToPrint = new();
  public string GoodsCodeToAddToQueue { get; set; } = "";

  protected override async Task OnInitializedAsync()
  {
    using PriceTagContext priceTagContext = PriceTagContextFactory.CreateDbContext();
    PriceTagsToPrint = await priceTagContext.PriceTags.Where(p => p.NeedsPrinting).ToListAsync();
  }

  public async Task HandleAddToQueue()
  {
    using GoodsContext goodsContext = GoodsContextFactory.CreateDbContext();
    using PriceTagContext priceTagContext = PriceTagContextFactory.CreateDbContext();
    Goods? goods = await goodsContext.FindAsync<Goods>(GoodsCodeToAddToQueue);
    if (goods is null)
    {
      GoodsCodeToAddToQueue = "";
      PlayErrorSound();
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
        PlayErrorSound();
        await JsRuntime.InvokeVoidAsync("alert", "That price tag is already scheduled to print!");
        return;
      }
      PlaySuccessSound();
      PriceTagsToPrint.Add(priceTag);
      priceTag.NeedsPrinting = true;
      priceTag.CreatedAt = DateTime.Now;
      await priceTagContext.SaveChangesAsync();
      GoodsCodeToAddToQueue = "";
      return;
    }
    PlaySuccessSound();
    priceTag = PriceTagGoodsConverter.ToPriceTag(goods);
    PriceTagsToPrint.Add(priceTag);

    await priceTagContext.PriceTags.AddAsync(priceTag);
    await priceTagContext.SaveChangesAsync();
    GoodsCodeToAddToQueue = "";
  }

  public async Task HandleChangePriceTagSize(string goodsCode, ChangeEventArgs changeEventArgs)
  {
    Console.WriteLine($"Change Price Tag Size: Code={goodsCode} Size={changeEventArgs.Value}");

    PriceTag? priceTag = PriceTagsToPrint.Where(p => p.GoodsCode == goodsCode).FirstOrDefault();
    if (priceTag is null) return;

    bool wasSuccess = Enum.TryParse(changeEventArgs.Value?.ToString(), out PriceTagSize size);
    if (wasSuccess == false) return;

    priceTag.Size = size;
    using PriceTagContext priceTagContext = PriceTagContextFactory.CreateDbContext();
    priceTagContext.Update(priceTag);
    await priceTagContext.SaveChangesAsync();

  }

  public async Task HandleRemovePriceTag(PriceTag priceTag)
  {
    using PriceTagContext priceTagContext = PriceTagContextFactory.CreateDbContext();
    PriceTagsToPrint.Remove(priceTag);
    priceTag.NeedsPrinting = false;
    priceTagContext.PriceTags.Update(priceTag);
    await priceTagContext.SaveChangesAsync();
  }

  public async Task HandlePrintAll()
  {
    await JsRuntime.InvokeVoidAsync("open", $"print-tags", "_blank");
  }

  public async Task HandleClearQueue()
  {
    using PriceTagContext priceTagContext = PriceTagContextFactory.CreateDbContext();
    while (PriceTagsToPrint.Count > 0)
    {
      RemovePriceTagFromContext(PriceTagsToPrint[0], priceTagContext);
    }
    await priceTagContext.SaveChangesAsync();
  }

  private void RemovePriceTagFromContext(PriceTag priceTag, PriceTagContext context)
  {
    PriceTagsToPrint.Remove(priceTag);
    priceTag.NeedsPrinting = false;
    context.PriceTags.Update(priceTag);
  }

  private async void PlaySuccessSound()
  {
    await JsRuntime.InvokeVoidAsync("PlayAudio", "blip1");
  }

  private async void PlayErrorSound()
  {
    await JsRuntime.InvokeVoidAsync("PlayAudio", "blip2");
  }
}