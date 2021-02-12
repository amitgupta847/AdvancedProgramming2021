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
    private ConcurrentDictionary<string, int> _stockMap = new ConcurrentDictionary<string, int>();

    int _totalQuantityBought;
    int _totalQuantitySold;

    public StockController() //IEnumerable<TShirt> shirts
    {
      //_stockMap = new ConcurrentDictionary<string, TShirt>(shirts.ToDictionary(item => item.Code));
    }

    //not thread safe
    //public void BuyShirts(string code, int quantityToBuy)
    //{
    //  if (!_stockMap.ContainsKey(code))
    //    _stockMap.TryAdd(code, 0);

    //  _stockMap[code] = _stockMap[code] + quantityToBuy;
    //  _totalQuantityBought = _totalQuantityBought + quantityToBuy;
    //}

    public void BuyShirts(string code, int quantityToBuy)
    {
      _stockMap.AddOrUpdate(code, quantityToBuy, (key, oldVal) => oldVal + quantityToBuy);
      Interlocked.Add(ref _totalQuantityBought, quantityToBuy);
    }

    //not thread safe
    //public bool TrySellShirt(string code)
    //{
    //  if (_stockMap.TryGetValue(code, out int stock) && stock > 0)
    //  {
    //    --_stockMap[code];
    //    ++_totalQuantitySold;
    //    return true;
    //  }
    //  else
    //    return false;
    //}


    //Refer notes to know the reason we have a complicated lambda with closure
    //actually intention is to achieve the opearaion in single call and also need to know if really we sold the shirt or not
    public bool TrySellShirt(string code)
    {
      bool success = false;
      int newStockLevel = _stockMap.AddOrUpdate(code,
        (itemName) => { success = false; return 0; },
        (itemName, oldValue) =>
        {
          if (oldValue == 0)
          {
            success = false;
            return 0;
          }
          else
          {
            success = true;
            return oldValue - 1;
          }
        });
      if (success)
        Interlocked.Increment(ref _totalQuantitySold);
      return success;
    }

    public void DisplayStock()
    {
      Console.WriteLine("Stock levels by item:");
      foreach (TShirt shirt in TShirtProvider.AllShirts)
      {
        // _stockMap.TryGetValue(shirt.Code, out int stockLevel);
        //just better to use GetOrAdd instead of TryGetValue
        int stockLevel= _stockMap.GetOrAdd(shirt.Code, 0);
        Console.WriteLine($"{shirt.Name,-30}: {stockLevel}");
      }

      int totalStock = _stockMap.Values.Sum();
      Console.WriteLine($"\r\nBought = {_totalQuantityBought}");
      Console.WriteLine($"Sold   = {_totalQuantitySold}");
      Console.WriteLine($"Stock  = {totalStock}");
      int error = totalStock + _totalQuantitySold - _totalQuantityBought;
      if (error == 0)
        Console.WriteLine("Stock levels match");
      else
        Console.WriteLine($"Error in stock level: {error}");
    }
  }

}
