using OOPs_And_DesignPatterns.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample
{
  public class aAirCraft_StatePattern
  {
    public static void StartMethod()
    {

      F16 f16 = new F16();
      f16.StartsEngine();
      f16.ParksPlane();
      f16.FliesPlane();  //you can not go to fly mode from parked.
      f16.EjectsPlane();

    }

  }
}
