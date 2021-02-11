using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections
{
  public class SimulateOrder1
  {

    //Entry function 
    public static void Start()
    {
      var orderQueue = new ConcurrentQueue<String>(); //Queue<string>();


      Task task1 = Task.Run(() => PlaceOrders(orderQueue, "Xavier", 5));
      Task task2 = Task.Run(() => PlaceOrders(orderQueue, "Ramdevi", 5));

      Task.WaitAll(task1, task2);

      foreach (var order in orderQueue)
      {
        Console.WriteLine($"Order: {order}");
      }

    }

   // private static Object syncRoot = new object();

    public static void PlaceOrders(ConcurrentQueue<string> orders, string customerName, int nOrders)
    {
      for (int i = 1; i <= nOrders; i++)
      {
        //Thread.Sleep(1);   //its in milliseconds.   1 = 1/1000 seconds
        
        Thread.Sleep(new TimeSpan(1)); // its in nano seconds    1 = 1/1000 millisecond    way shorter

        string orderName = $"{customerName} wants t-shirt {i}";
        orders.Enqueue(orderName);
      }
    }

  }


}
