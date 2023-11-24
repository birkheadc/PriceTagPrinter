namespace PriceTagPrinter.Models;

public static class PriceTagGoodsConverter
{
  public static PriceTag ToPriceTag(Goods goods)
  {
    return new PriceTag()
    {
      GoodsCode = goods.GoodsCode,
      GoodsName = goods.GoodsName,
      GoodsPrice = goods.GoodsCost,
      CreatedAt = DateTime.Now,
      Size = PriceTagSize.NORMAL,
      NeedsPrinting = true
    };
  }
}