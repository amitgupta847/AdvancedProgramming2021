using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.zCaseStudies.aParkingLotDesign
{
  public abstract class ParkingSpot
  {
    public string Number { get; private set; }
    private bool free;
    private Vehicle vehicle;
    public ParkingSpotType Type { get; private set;}

    public ParkingSpot(ParkingSpotType type)
    {
      Type = type;
    }

    public bool IsFree()
    {
      //TODO:
      return true;
    }


    public bool assignVehicle(Vehicle vehicle)
    {
      this.vehicle = vehicle;
      free = false;
      return true;
    }

    public bool removeVehicle()
    {
      this.vehicle = null;
      free = true;
      return true;
    }
  }

  public class HandicappedSpot : ParkingSpot
  {
    public HandicappedSpot() : base(ParkingSpotType.HANDICAPPED)
    {

    }
  }

  public class CompactSpot : ParkingSpot
  {
    public CompactSpot() : base(ParkingSpotType.COMPACT)
    {

    }
  }

  public class LargeSpot : ParkingSpot
  {
    public LargeSpot() : base(ParkingSpotType.LARGE)
    {

    }
  }

  public class MotorbikeSpot : ParkingSpot
  {
    public MotorbikeSpot() : base(ParkingSpotType.MOTORBIKE)
    {

    }
  }

  public class ElectricSpot : ParkingSpot
  {
    public ElectricSpot() : base(ParkingSpotType.ELECTRIC)
    {

    }
  }
}
