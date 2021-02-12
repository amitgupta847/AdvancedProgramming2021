using Collections.C2_ConcurrentCollections.Shop_Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.Ex2_TShirtShopBuyAndSell
{
  public class SimulateShop
  {

    //Entry function 
    public static void Start()
    {
      StockController controller = new StockController();
      

      //This TimeSpan, workDay, tells us how long each sales person will work for. 
      TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);
      SalesPerson kim = new SalesPerson("Kim");
      // SalesPerson sahil = new SalesPerson("Sahil");
      // SalesPerson chuck = new SalesPerson("Chuck");

      kim.Work(workDay, controller);

      //Task task1 = Task.Run(() => kim.Work(workDay, controller));
      // Task task2 = Task.Run(() => sahil.Work(workDay, controller));
      //Task task3 = Task.Run(() => chuck.Work(workDay, controller));


      // Task.WaitAll(task1, task2, task3);

      controller.DisplayStock();
    }
  }

}
