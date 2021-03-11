using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample
{


  public class ParkedState : IPilotActions
  {
    F16 f16;

    // Notice, how the state class is composed with the context object
    public ParkedState(F16 f16)
    {
      this.f16 = f16;
    }

    public void pilotTaxies(F16 f16)
    {
      f16.SetState(f16.TaxiState);
      Console.WriteLine($"Moving from {this.ToString()} to {f16.CurrentState.ToString()}");
    }
    public void pilotFlies(F16 f16)
    {
      Console.WriteLine("This is an invalid operation for this state");
    }

    public void pilotEjects(F16 f16)
    {
      f16.SetState(f16.CrashState);
      Console.WriteLine($"Moving from {this.ToString()} to {f16.CurrentState.ToString()}");
    }

    public void pilotLands(F16 f16)
    {
      Console.WriteLine("This is an invalid operation for this state");
    }


    public void pilotParks(F16 f16)
    {
      Console.WriteLine("This is an invalid operation for this state");
    }

    public override string ToString()
    {
      return this.GetType().Name;
    }
  }

}
