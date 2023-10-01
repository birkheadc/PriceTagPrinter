using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceTagPrinter.Models;

[Table("TM_GOODS")]
public class Goods
{
  [Key]
  [Column("GOODS_CODE")]
  public string GoodsCode { get; set; } = "";
  [Column("GOODS_NAME")]
  public string GoodsName { get; set; } = "";
  [Column("GOODS_PRICE")]
  public int GoodsCost { get; set; } = 0;
}