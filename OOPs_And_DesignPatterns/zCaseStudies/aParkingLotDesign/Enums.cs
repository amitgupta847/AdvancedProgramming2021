using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.zCaseStudies.aParkingLotDesign
{
  public enum VehicleType
  {
    CAR, TRUCK, ELECTRIC, VAN, MOTORBIKE
  }

  public enum ParkingSpotType
  {
    HANDICAPPED, COMPACT, LARGE, MOTORBIKE, ELECTRIC
  }

  public enum AccountStatus
  {
    ACTIVE, BLOCKED, BANNED, COMPROMISED, ARCHIVED, UNKNOWN
  }

  public enum ParkingTicketStatus
  {
    ACTIVE, PAID, LOST
  }
}
