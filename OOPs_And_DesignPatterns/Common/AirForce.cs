using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.Common
{
  public abstract class AirCraft
  {
    public int Fuel { get; protected set; }

    public abstract void Land();

  }

  /// <summary>
  /// Air force with collection of different planes.
  /// </summary>
  public class AirForce
  {
    public List<AirCraft> Crafts = new List<AirCraft>();

    public AirForce()
    {
      Crafts.Add(new F16());
      Crafts.Add(new Boeing747());
      Crafts.Add(new Fighter74());
    }
  }

  public class F16 : AirCraft
  {
    public F16()
    {
      Fuel = 100;
    }

    public override void Land()
    {
      Console.WriteLine("F16 Landing...");
    }

    public override string ToString()
    {
      return "F16";
    }
  }

  public class Boeing747 : AirCraft
  {
    public Boeing747()
    {
      Fuel = 200;
    }

    public override void Land()
    {
      Console.WriteLine("Boeing747 Landing...");
    }

    public override string ToString()
    {
      return "Boeing747";
    }
  }

  public class Fighter74 : AirCraft
  {
    public Fighter74()
    {
      Fuel = 200;
    }

    public override void Land()
    {
      Console.WriteLine("Fighter74 Landing...");
    }

    public override string ToString()
    {
      return "Fighter74";
    }
  }
}
