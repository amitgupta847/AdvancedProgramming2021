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
  public class StockController
  {
    private ConcurrentDictionary<string, TShirt> _stockMap;

    public StockController(IEnumerable<TShirt> shirts)
    {
      _stockMap = new ConcurrentDictionary<string, TShirt>(shirts.ToDictionary(item => item.Code));
    }

    public bool Sell(string code)
    {
      return _stockMap.TryRemove(code, out TShirt shirtRemoved);
    }

    public (SelectResult result, TShirt shirt) SelectRandomShirt()
    {
      var keys = _stockMap.Keys.ToList();

      if (keys.Count == 0)
        return (SelectResult.NoStockLeft, null);   // shirts sold

      //Method sleeps for a short time chosen randomly up to 10 ms.
      //You can think of that as the customer making up his mind which shirt he wants to browse, 
      //but the real purpose is to make the thread timings a bit more realistic because in real life, 
      //most apps would be doing far more processing. After sleeping, the code simply randomly selects one of the keys in the list and looks up and returns the T‑shirt with that code.
      Thread.Sleep(Rnd.NextInt(10));

      string selectedCode = keys[Rnd.NextInt(keys.Count)];

      if (_stockMap.TryGetValue(selectedCode, out TShirt shirt))
        return (SelectResult.Success, shirt);
      else
        return (SelectResult.ChosenShirtSold, null);

      //return _stockMap[selectedCode];
    }


    //Below code is not thread save at when we try to access the dictionary, the item might have removed
    //public TShirt SelectRandomShirt()
    //{
    //  var keys = _stockMap.Keys.ToList();

    //  if (keys.Count == 0)
    //    return null;   // shirts sold

    //  //Method sleeps for a short time chosen randomly up to 10 ms.
    //  //You can think of that as the customer making up his mind which shirt he wants to browse, 
    //  //but the real purpose is to make the thread timings a bit more realistic because in real life, 
    //  //most apps would be doing far more processing. After sleeping, the code simply randomly selects one of the keys in the list and looks up and returns the T‑shirt with that code.
    //  Thread.Sleep(Rnd.NextInt(10));

    //  string selectedCode = keys[Rnd.NextInt(keys.Count)];
    //  return _stockMap[selectedCode];
    //}

    public void DisplayStock()
    {
      Console.WriteLine($"\r\n{_stockMap.Count} items left in stock:");
      foreach (TShirt shirt in _stockMap.Values)
        Console.WriteLine(shirt);
    }
  }

  public enum SelectResult { Success, NoStockLeft, ChosenShirtSold }

}
