namespace PriceTagPrinter.Models;

public class PriceTag
{
  public string GoodsCode { get; set; } = "";
  public string GoodsName { get; set; } = "";
  public int GoodsPrice { get; set; } = 0;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}