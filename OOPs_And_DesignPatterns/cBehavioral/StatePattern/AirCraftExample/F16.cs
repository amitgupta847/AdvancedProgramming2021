using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample
{

  public interface IAircraft
  {   //Empty interface
  }

  public class F16 : IAircraft
  {

    public ParkedState ParkedState { get; private set; }
    public CrashState CrashState { get; private set; }
    public LandState LandState { get; private set; }
    public TaxiState TaxiState { get; private set; }
    public AirborneState AirborneState { get; private set; }

   public IPilotActions CurrentState { get; private set; }

    public F16()
    {
      ParkedState = new ParkedState(this);
      CrashState = new CrashState(this);
      LandState = new LandState(this);
      TaxiState = new TaxiState(this);
      AirborneState = new AirborneState(this);

      CurrentState = ParkedState;
    }

    public void StartsEngine()
    {
      CurrentState.pilotTaxies(this);
    }

    public void FliesPlane()
    {
      CurrentState.pilotFlies(this);
    }

    public void LandsPlane()
    {
      CurrentState.pilotLands(this);
    }

    public void EjectsPlane()
    {
      CurrentState.pilotEjects(this);
    }

    public void ParksPlane()
    {
      CurrentState.pilotParks(this);
    }

    public void SetState(IPilotActions IPilotActions)
    {
      CurrentState = IPilotActions;
    }
  }
}
