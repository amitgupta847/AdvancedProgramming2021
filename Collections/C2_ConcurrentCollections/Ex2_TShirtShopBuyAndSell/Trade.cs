using Collections.C2_ConcurrentCollections.Shop_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.Ex2_TShirtShopBuyAndSell
{
  public class Trade
  {
    public SalesPerson Person { get; private set; }

    public TShirt Shirt { get; private set; }

    public int Quantity { get; private set; }

    public TradeType Type { get; private set; }

    public bool IsSale => Type == TradeType.Sale;

    public Trade(SalesPerson person, TShirt shirt, TradeType type, int quantitySold)
    {
      Person = person;
      Shirt = shirt;
      Quantity = quantitySold;
      Type = type;
    }

    public override string ToString()
    {
      string typeText = IsSale ? "bought" : "sold";
      return $"{Person} {typeText} {Quantity} {Shirt.Name}";
    }
  }

  public enum TradeType { Sale, Purchase }
}
