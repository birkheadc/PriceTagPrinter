using System.ComponentModel.DataAnnotations;

namespace PriceTagPrinter.Models;

public class PriceTag
{
  [Key]
  public string GoodsCode { get; set; } = "";
  public string GoodsName { get; set; } = "";
  public int GoodsPrice { get; set; } = 0;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public PriceTagSize Size { get; set; } = PriceTagSize.NORMAL;
  public bool NeedsPrinting { get; set; } = true;
}