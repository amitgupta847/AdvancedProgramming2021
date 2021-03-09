using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.VisitorPattern.AirCraftExample
{

  //In this class we created a simple interface with accept method and created all subclasses against it.

  /// <summary>
  /// Entry Point
  /// </summary>
  public class AirCraft_V1_AsVisitorPattern
  {
    public static void StartMethod()
    {
      AirForce airForce = new AirForce(); // a collection of planes where each plane is visitable.

      MetricsVisitor metricVisitor = new MetricsVisitor(); //a class that would perform an operation on all the objects that are visistable. This class is the one which providing behavior we intend to get from all the objects.

      foreach (IAirCraft craft in airForce.GetIterator())
      {
        craft.Accept(metricVisitor);
      }

      Console.WriteLine($"Total fuel need by all the planes is: {metricVisitor.TotalFuelNeeded}");
    }
  }


  public interface IAirCraft
  {
    // Each concrete element class is expected to
    // define the accept method
    public void Accept(IAirCraftVisitor visitor);
  }

  public interface IAirCraftVisitor
  {
    void Visit(F16 f16);
    void Visit(Boeing747 b747);

    //every time you introduce a new type, you need to change this interface to have a Visit method with that type as a parameter
  }


  /// <summary>
  /// We will return the total fuel in each plane as the metricss
  /// </summary>
  public class MetricsVisitor : IAirCraftVisitor
  {
    public int TotalFuelNeeded { get; private set; }
    public void Visit(F16 f16)
    {
      TotalFuelNeeded = TotalFuelNeeded + f16.FuelNeeded;
    }

    public void Visit(Boeing747 b747)
    {
      TotalFuelNeeded = TotalFuelNeeded + b747.FuelNeeded;
    }
  }


  /// <summary>
  /// Air force with collection of different planes, where each place is visitable by implementing the IAirCraft interface. (We could als have seperate interface with name IVisitable)
  /// </summary>
  public class AirForce
  {
    private List<IAirCraft> planes = new List<IAirCraft>();
    public AirForce()
    {
      planes.Add(new F16());
      planes.Add(new Boeing747());
    }

    public IEnumerable<IAirCraft> GetIterator()
    {
      return planes;
    }
  }

  public class F16 : IAirCraft
  {
    public F16()
    {
      FuelNeeded = 100;
    }
    public int FuelNeeded { get; set; }

    public void Accept(IAirCraftVisitor visitor)
    {
      visitor.Visit(this);
    }
  }

  public class Boeing747 : IAirCraft
  {
    public Boeing747()
    {
      FuelNeeded = 200;
    }
    public int FuelNeeded { get; set; }
    public void Accept(IAirCraftVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
}
