using System;
using System.Collections.Generic;
using System.Text;

namespace OOPs_And_DesignPatterns.cBehavioral.CommandPattern.AirCraftExample
{
  public class AirCraft_V1_AsCommandPattern
  {
    public static void StartMethod()
    {
      //Framework/or Client
      InstrmentPanel panel = new InstrmentPanel();

      //Receiver
      LandingGear gears = new LandingGear(new F16());

      //Commands
      panel.SetCommand(0, new LandingGearUpCommand(gears));
      panel.SetCommand(1, new LandingGearDownCommand(gears));

      //use the panel. Panel is not aware of receiver. It just know about commands and their execute method.
      panel.Button1_Up();
      panel.Button2_Down();
    }
  }

  /// <summary>
  /// Below is the instrument panel which we are trying to configure using  command pattern.
  /// </summary>
  public class InstrmentPanel
  {
    const int Max_Buttons = 2;
    ICommand[] commands = new ICommand[Max_Buttons];

    public void SetCommand(int commandIndex, ICommand cmd)
    {
      if (commandIndex >= Max_Buttons)
        throw new ArgumentException($"Total {Max_Buttons}  buttons are available to configure");

      commands[commandIndex] = cmd;
    }

    public void Button1_Up()
    {
      if (commands[0] != null)
        commands[0].Execute();
      else
        Console.WriteLine("Button 1 is not configured yet");
    }
    public void Button2_Down()
    {
      if (commands[1] != null)
        commands[1].Execute();
      else
        Console.WriteLine("Button 2 is not configured yet");
    }
  }


  public interface ICommand
  {
    void Execute();
  }

  public class LandingGear
  {
    private AirCraft craft;
    public LandingGear(AirCraft craft)
    {
      this.craft = craft;
    }

    public void LandingUp()
    {
      this.craft.LandingUp();
    }

    public void LandingDown()
    {
      this.craft.LandingDown();
    }
  }

  //Command 1
  public class LandingGearUpCommand : ICommand
  {
    //Receiver
    public LandingGear landingGear;

    //encapsulates the receiver
    public LandingGearUpCommand(LandingGear reciever)
    {
      this.landingGear = reciever;
    }

    public void Execute()
    {
      landingGear.LandingUp();

      //you can do any logging here, or may be store the command in a list to do undo or something if command support that.
    }
  }

  //Command 2
  public class LandingGearDownCommand : ICommand
  {
    //Receiver
    public LandingGear landingGear;

    //encapsulates the receiver
    public LandingGearDownCommand(LandingGear reciever)
    {
      this.landingGear = reciever;
    }

    public void Execute()
    {
      landingGear.LandingDown();

      //you can do any logging here, or may be store the command in a list to do undo or something if command support that.
    }
  }


  public abstract class AirCraft
  {
    public int Fuel { get; protected set; }

    public abstract void LandingUp();

    public abstract void LandingDown();

  }

  /// <summary>
  /// Air force with collection of different planes.
  /// </summary>
  public class AirForce
  {
    private List<AirCraft> planes = new List<AirCraft>();

    public AirForce()
    {
      planes.Add(new F16());
      planes.Add(new Boeing747());
    }
  }

  public class F16 : AirCraft
  {
    public F16()
    {
      Fuel = 100;
    }

    public override void LandingDown()
    {
      Console.WriteLine("F16 Landing Down");
    }

    public override void LandingUp()
    {
      Console.WriteLine("F16 Landing Up");
    }
  }

  public class Boeing747 : AirCraft
  {
    public Boeing747()
    {
      Fuel = 200;
    }

    public override void LandingDown()
    {
      Console.WriteLine("Boeing747 Landing Down");
    }

    public override void LandingUp()
    {
      Console.WriteLine("Boeing747 Landing Up");
    }
  }
}
