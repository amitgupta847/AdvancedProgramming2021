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
    private Dictionary<string, int> _stockMap = new Dictionary<string, int>();

    int _totalQuantityBought;
    int _totalQuantitySold;

    public StockController() //IEnumerable<TShirt> shirts
    {
      //_stockMap = new ConcurrentDictionary<string, TShirt>(shirts.ToDictionary(item => item.Code));
    }

    public void BuyShirts(string code, int quantityToBuy)
    {
      if (!_stockMap.ContainsKey(code))
        _stockMap.Add(code, 0);

      _stockMap[code] = _stockMap[code] + quantityToBuy;
      _totalQuantityBought = _totalQuantityBought + quantityToBuy;

    }

    public bool TrySellShirt(string code)
    {
      if (_stockMap.TryGetValue(code, out int stock) && stock > 0)
      {
        --_stockMap[code];
        ++_totalQuantitySold;
        return true;
      }
      else
        return false;
    }

    public void DisplayStock()
    {
      Console.WriteLine("Stock levels by item:");
      foreach (TShirt shirt in TShirtProvider.AllShirts)
      {
        _stockMap.TryGetValue(shirt.Code, out int stockLevel);
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

  public enum SelectResult { Success, NoStockLeft, ChosenShirtSold }

}
