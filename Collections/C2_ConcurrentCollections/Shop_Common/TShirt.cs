using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.Shop_Common
{
  public class TShirt
  {
    public string Code { get; }
    public string Name { get; }
    public int PricePence { get; }

    public TShirt(string code, string name, int pricePence)
    {
      Code = code;
      Name = name;
      PricePence = pricePence;
    }

    public override string ToString()
    {
      return $"{Name} ({DisplayPrice(PricePence)}) ";
    }

    private string DisplayPrice(int pricePence)
    {
      return $"${pricePence / 100}.{pricePence % 100:00}";
    }
  }
}
