using System;
using System.Collections.Generic;
using System.Text;
using OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample;

namespace OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample
{

  /// <summary>
  /// This interface defines the actions a pilot can take against the aircraft object. 
  /// Each action will move the aircraft into a different state
  /// </summary>
  public interface IPilotActions
  {
    void pilotTaxies(F16 f16);

    void pilotFlies(F16 f16);

    void pilotEjects(F16 f16);

    void pilotLands(F16 f16);

    void pilotParks(F16 f16);
  }
}
