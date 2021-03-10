using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.VisitorPattern.AirCraftExample3
{

  //In this class i am trying to create abstract class for aircraft and then a separate interface for IVisitable

  /// <summary>
  /// Entry Point
  /// </summary>
  public class AirCraft_V3_DoubleDispatcher
  {
    public static void StartMethod()
    {

      F16 f16 = new F16();
      F16 betterF16 = new BetterF16();

      Missile missile = new Missile();
      Missile betterMissile = new BetterMissile();

      Console.WriteLine("Expected output");
      f16.fireMissile(missile);
      betterF16.fireMissile(missile);
      Console.WriteLine();

      Console.WriteLine("Failed double dispatch attempt");
      f16.fireMissile(betterMissile);
      betterF16.fireMissile(betterMissile);
      Console.WriteLine();

      Console.WriteLine("Expected output");
      BetterMissile reallyBetterMissile = new BetterMissile();
      f16.fireMissile(reallyBetterMissile);
      betterF16.fireMissile(reallyBetterMissile);
      Console.WriteLine();

      return;
    }
  }

  public class F16
  {
    public virtual String WhoAmI()
    {
      return "F16";
    }

    public void fireMissile(Missile missile)
    {
      Console.WriteLine(WhoAmI() + " fired ordinary missile: " + missile.Explode());
    }

    public void fireMissile(BetterMissile missile)
    {
      Console.WriteLine(WhoAmI() + " fired better missile: " + missile.Explode());
    }

  }

  public class BetterF16 : F16
  {
    public override String WhoAmI()
    {
      return "Better F16";
    }
  }

  public class Missile
  {
    public virtual string Explode()
    {
      return "baaaam";
    }
  }

  public class BetterMissile : Missile
  {
    public override string Explode()
    {
      return "very very big baaaam";
    }
  }


}
