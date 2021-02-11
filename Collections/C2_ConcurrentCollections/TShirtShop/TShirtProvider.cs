using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C2_ConcurrentCollections.TShirtShop
{
  public static class TShirtProvider
  {

    //Total 8 tshirts are provided at start.
    public static ImmutableArray<TShirt> AllShirts { get; } = ImmutableArray.Create(
      new TShirt("igeek", "IGeek", 500),
      new TShirt("bigdata", "Big Data", 600),
      new TShirt("ilovenode", "I Love Node", 750),
      new TShirt("kcdc", "kcdc", 400),
      new TShirt("docker", "Docker", 350),
      new TShirt("qcon", "QCon", 300),
      new TShirt("ps", "Pluralsight", 60000),
      new TShirt("pslive", "Pluralsight Live", 60000)
    );
  }
}
