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
        var result = ServeCustomer(controller);
        if (result.status != null)
          Console.WriteLine($"{Name}:  {result.status}");

        if (!result.shirtsInStock)
          break;
      }
    }

    public (bool shirtsInStock, string status) ServeCustomer(StockController controller)
    {
      var result = controller.SelectRandomShirt();
      if (result.result == SelectResult.NoStockLeft)
        return (false, "All shirts sold");
      else if (result.result == SelectResult.ChosenShirtSold)
        return (true, "Can't show shirt to customer - already sold");


      Thread.Sleep(Rnd.NextInt(30));
      TShirt shirt = result.shirt;
      //customer chooses to buy with only 20% probability
      if (Rnd.TrueWithProb(0.2))
      {
        bool isSold = controller.Sell(shirt.Code);

        if (isSold)
          return (true, $"Sold {shirt.Name}");
        else
          return (true, $"Can't sell {shirt.Name}: Already Sold");
      }

      return (true, null);
    }

  }
}
