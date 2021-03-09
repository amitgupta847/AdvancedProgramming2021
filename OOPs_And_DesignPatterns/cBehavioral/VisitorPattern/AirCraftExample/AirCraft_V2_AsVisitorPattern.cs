using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.VisitorPattern.AirCraftExample2
{

  //In this class i am trying to create abstract class for aircraft and then a separate interface for IVisitable

  /// <summary>
  /// Entry Point
  /// </summary>
  public class AirCraft_V2_AsVisitorPattern
  {
    public static void StartMethod()
    {
      AirForce airForce = new AirForce(); // a collection of planes where each plane is visitable.

      MetricsVisitor metricVisitor = new MetricsVisitor(); //a class that would perform an operation on all the objects that are visistable. This class is the one which providing behavior we intend to get from all the objects.

      foreach (IVisitableAirCraft visitAbleCraft in airForce.GetVisitableIterator())
      {
        visitAbleCraft.Accept(metricVisitor);
      }

      Console.WriteLine($"Total fuel need by all the planes is: {metricVisitor.TotalFuelNeeded}");
    }
  }


  public abstract class AirCraft : IVisitableAirCraft
  {
    public int Fuel { get; protected set; }

    public abstract void Accept(IAirCraftVisitor visitor);
  }


  public interface IVisitableAirCraft
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
      TotalFuelNeeded = TotalFuelNeeded + f16.Fuel;
    }

    public void Visit(Boeing747 b747)
    {
      TotalFuelNeeded = TotalFuelNeeded + b747.Fuel;
    }
  }


  /// <summary>
  /// Air force with collection of different planes, where each place is visitable by implementing the IAirCraft interface. (We could als have seperate interface with name IVisitable)
  /// </summary>
  public class AirForce
  {
    private List<AirCraft> planes = new List<AirCraft>();

    public AirForce()
    {
      planes.Add(new F16());
      planes.Add(new Boeing747());
    }

    public IEnumerable<IVisitableAirCraft> GetVisitableIterator()
    {
      return planes.OfType<IVisitableAirCraft>();
    }
  }

  public class F16 : AirCraft
  {
    public F16()
    {
      Fuel = 100;
    }

    public override void Accept(IAirCraftVisitor visitor)
    {
      visitor.Visit(this);
    }
  }

  public class Boeing747 : AirCraft
  {
    public Boeing747()
    {
      Fuel = 200;
    }

   public override void Accept(IAirCraftVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
}
