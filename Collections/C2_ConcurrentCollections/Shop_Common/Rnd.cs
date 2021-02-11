using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.Shop_Common
{
  public class Rnd
  {
    private static Random _generator = new Random();

    public static int NextInt(int max) => _generator.Next(max);

    public static bool TrueWithProb(double probOfTrue) => _generator.NextDouble() < probOfTrue;


  }
}
