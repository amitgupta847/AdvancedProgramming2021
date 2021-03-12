using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.zCaseStudies.aParkingLotDesign
{
  public class ParkingFloor
  {
    private String name;
    private Dictionary<String, HandicappedSpot> handicappedSpots;
    private Dictionary<String, CompactSpot> compactSpots;
    private Dictionary<String, LargeSpot> largeSpots;
    private Dictionary<String, MotorbikeSpot> motorbikeSpots;
    private Dictionary<String, ElectricSpot> electricSpots;
    private Dictionary<String, CustomerInfoPortal> infoPortals;
    private ParkingDisplayBoard displayBoard;

    public ParkingFloor(String name)
    {
      this.name = name;
    }

    public void addParkingSpot(ParkingSpot spot)
    {
      switch (spot.Type)
      {
        case ParkingSpotType.HANDICAPPED:
          handicappedSpots[spot.Number] = spot as HandicappedSpot;
          break;
        case ParkingSpotType.COMPACT:
          compactSpots[spot.Number] = spot as CompactSpot;
          break;
        case ParkingSpotType.LARGE:
          largeSpots[spot.Number] = spot as LargeSpot;
          break;
        case ParkingSpotType.MOTORBIKE:
          motorbikeSpots[spot.Number] = spot as MotorbikeSpot;
          break;
        case ParkingSpotType.ELECTRIC:
          electricSpots[spot.Number] = spot as ElectricSpot;
          break;
        default:
          Console.WriteLine("Wrong parking spot type!");
          break;
      }
    }

    public void assignVehicleToSpot(Vehicle vehicle, ParkingSpot spot)
    {
      spot.assignVehicle(vehicle);
      switch (spot.Type)
      {
        case ParkingSpotType.HANDICAPPED:
          updateDisplayBoardForHandicapped(spot);
          break;
        case ParkingSpotType.COMPACT:
          updateDisplayBoardForCompact(spot);
          break;
        case ParkingSpotType.LARGE:
          //updateDisplayBoardForLarge(spot);
          break;
        case ParkingSpotType.MOTORBIKE:
          //updateDisplayBoardForMotorbike(spot);
          break;
        case ParkingSpotType.ELECTRIC:
          // updateDisplayBoardForElectric(spot);
          break;
        default:
          Console.WriteLine("Wrong parking spot type!");
          break;
      }
    }

    private void updateDisplayBoardForHandicapped(ParkingSpot spot)
    {
      //if (this.displayBoard.getHandicappedFreeSpot().getNumber() == spot.getNumber())
      //{
      //  // find another free handicapped parking and assign to displayBoard
      //  for (String key : handicappedSpots.keySet())
      //  {
      //    if (handicappedSpots.get(key).isFree())
      //    {
      //      this.displayBoard.setHandicappedFreeSpot(handicappedSpots.get(key));
      //    }
      //  }
      //  this.displayBoard.showEmptySpotNumber();
      //}
    }

    private void updateDisplayBoardForCompact(ParkingSpot spot)
    {
      //if (this.displayBoard.getCompactFreeSpot().getNumber() == spot.getNumber())
      //{
      //  // find another free compact parking and assign to displayBoard
      //  for (String key : compactSpots.keySet())
      //  {
      //    if (compactSpots.get(key).isFree())
      //    {
      //      this.displayBoard.setCompactFreeSpot(compactSpots.get(key));
      //    }
      //  }
      //  this.displayBoard.showEmptySpotNumber();
      //}
    }

    public void freeSpot(ParkingSpot spot)
    {
      spot.removeVehicle();
      switch (spot.Type)
      {
        case ParkingSpotType.HANDICAPPED:
          // freeHandicappedSpotCount++;
          break;
        case ParkingSpotType.COMPACT:
          // freeCompactSpotCount++;
          break;
        case ParkingSpotType.LARGE:
          //freeLargeSpotCount++;
          break;
        case ParkingSpotType.MOTORBIKE:
          //freeMotorbikeSpotCount++;
          break;
        case ParkingSpotType.ELECTRIC:
          //freeElectricSpotCount++;
          break;
        default:
          Console.WriteLine("Wrong parking spot type!");
          break;
      }
    }
  }
}
