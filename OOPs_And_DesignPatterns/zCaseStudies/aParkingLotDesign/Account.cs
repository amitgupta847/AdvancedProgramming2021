using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.zCaseStudies.aParkingLotDesign
{
  public abstract class Account
  {
    private string userName;
    private string password;
    private AccountStatus status;
    private Person person;

    public abstract bool resetPassword();
  }

  public class Admin : Account
  {
    public bool addParkingFloor(ParkingFloor floor)
    {
      //TODO:
      return true;
    }
    public bool addParkingSpot(String floorName, ParkingSpot spot)
    {
      //TODO:
      return true;
    }

    public bool addParkingDisplayBoard(String floorName, ParkingDisplayBoard displayBoard)
    {
      //TODO:
      return true;
    }

    public bool addCustomerInfoPanel(String floorName, CustomerInfoPanel infoPanel)
    {
      //TODO:
      return true;
    }


    public bool addEntrancePanel(EntrancePanel entrancePanel) {
      //TODO:
      return true;
    }
    public bool addExitPanel(ExitPanel exitPanel)
    {
      //TODO:
      return true;
    }

    public override bool resetPassword()
    {
      throw new NotImplementedException();
    }
  }

  public class ParkingAttendant : Account
  {
    public bool processTicket(string TicketNumber)
    {
      //TODO:
      return true;
    }

    public override bool resetPassword()
    {
      throw new NotImplementedException();
    }
  }

}
