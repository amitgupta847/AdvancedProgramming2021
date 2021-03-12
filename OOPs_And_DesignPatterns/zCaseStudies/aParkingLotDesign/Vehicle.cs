using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.zCaseStudies.aParkingLotDesign
{
  public abstract class Vehicle
  {
    private string licenseNumber;
    public VehicleType Type { get; private set; }

    private ParkingTicket ticket;

    public Vehicle(VehicleType type)
    {
      Type = type;
    }

    public void assignTicket(ParkingTicket ticket)
    {
      this.ticket = ticket;
    }
  }

  public class Car : Vehicle
  {
    public Car() : base(VehicleType.CAR)
    {

    }
  }

  public class Van : Vehicle
  {
    public Van() : base(VehicleType.VAN)
    {

    }
  }

  public class Truck : Vehicle
  {
    public Truck() : base(VehicleType.TRUCK)
    {

    }
  }

}
