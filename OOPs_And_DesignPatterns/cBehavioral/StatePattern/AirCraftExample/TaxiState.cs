using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample
{


  public class TaxiState : IPilotActions
  {
    F16 f16;

    // Notice, how the state class is composed with the context object
    public TaxiState(F16 f16)
    {
      this.f16 = f16;
    }

    public void pilotTaxies(F16 f16)
    {
      Console.WriteLine("This is an invalid operation for this state");
    }
    public void pilotFlies(F16 f16)
    {
      f16.SetState(f16.AirborneState);
      Console.WriteLine($"Moving from {this.ToString()} to {f16.CurrentState.ToString()}");
    }

    public void pilotEjects(F16 f16)
    {
      Console.WriteLine("This is an invalid operation for this state");
    }

    public void pilotLands(F16 f16)
    {
      Console.WriteLine("This is an invalid operation for this state");
    }

    public void pilotParks(F16 f16)
    {
      f16.SetState(f16.ParkedState);
      Console.WriteLine($"Moving from {this.ToString()} to {f16.CurrentState.ToString()}");
    }

    public override string ToString()
    {
      return this.GetType().Name;
    }
  }

}
