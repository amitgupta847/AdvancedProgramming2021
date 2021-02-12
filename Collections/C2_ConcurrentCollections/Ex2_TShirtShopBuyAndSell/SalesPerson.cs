using Collections.C2_ConcurrentCollections.Shop_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.Ex2_TShirtShopBuyAndSell
{
  public class SalesPerson
  {
    public string Name { get; }

    public SalesPerson(string name)
    {
      Name = name;
    }

    public void Work(TimeSpan workDay, StockController controller)
    {
      DateTime start = DateTime.Now;
      while (DateTime.Now - start < workDay)
      {
        string msg = ServeCustomer(controller);
        if (msg != null)
          Console.WriteLine($"{Name}: {msg}");
      }
    }

    public string ServeCustomer(StockController controller)
    {
      Thread.Sleep(Rnd.NextInt(5));
      TShirt shirt = TShirtProvider.SelectRandomShirt();
      string code = shirt.Code;

      bool custSells = Rnd.TrueWithProb(1.0 / 6.0);
      if (custSells)
      {
        int quantity = Rnd.NextInt(9) + 1;
        controller.BuyShirts(code, quantity);
        return $"Bought {quantity} of {shirt}";
      }
      else
      {
        bool success = controller.TrySellShirt(code);
        if (success)
          return $"Sold {shirt}";
        else
          return $"Couldn't sell {shirt}: Out of stock";
      }
    }

  }
}
