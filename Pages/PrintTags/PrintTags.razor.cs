using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PriceTagPrinter.Contexts;
using PriceTagPrinter.Models;

namespace PriceTagPrinter.Pages;

public partial class PrintTags
{
  private PriceTagContext? priceTagContext;
  public List<List<PriceTag>> PriceTagPagesToPrint = new();
  protected override async Task OnInitializedAsync()
  {
    priceTagContext ??= await PriceTagContextFactory.CreateDbContextAsync();
    List<PriceTag> priceTags = await priceTagContext.PriceTags.Where(p => p.NeedsPrinting).ToListAsync();

    List<List<PriceTag>> priceTagPagesToPrint = BreakPriceTagsIntoBlocksOfN(priceTags, 25);
    PriceTagPagesToPrint = priceTagPagesToPrint;
  }

  private List<List<PriceTag>> BreakPriceTagsIntoBlocksOfN(List<PriceTag> priceTags, int n)
  {
    List<List<PriceTag>> pages = new();
    List<PriceTag> current = new();

    int i = 0;
    int j = 0;

    while (i < priceTags.Count)
    {
      if (j >= n)
      {
        j = 0;
        pages.Add(current);
        current = new();
      }

      current.Add(priceTags[i]);

      i++;
      j++;
    }

    pages.Add(current);

    return pages;
  }
}