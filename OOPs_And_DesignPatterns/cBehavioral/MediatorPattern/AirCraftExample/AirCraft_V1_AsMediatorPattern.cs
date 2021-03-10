using OOPs_And_DesignPatterns.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OOPs_And_DesignPatterns.cBehavioral.MediatorPattern.AirCraftExample
{
  public class AirCraft_V1_AsMediatorPattern
  {
    public static void StartMethod()
    {
      Console.WriteLine("Simulation for flight scheduling is going to start.......");
      Console.WriteLine("At any time Press enter to stop the simulation");
      
      Thread.Sleep(5000);
      
      AirCraft_V1_AsMediatorPattern pattern = new AirCraft_V1_AsMediatorPattern();
      Thread th = pattern.StartSimulation();

      
      Console.ReadLine();
      pattern.isDone = true;
      Console.WriteLine("Stopping simulation");
      th.Join();
      Console.WriteLine("Simultion stopped");
    }

    public volatile bool isDone = false;

    public Thread StartSimulation()
    {
      Thread th = new Thread(ScheduleFlights);
      th.Start();
      return th;
    }

    private void ScheduleFlights()
    {
      ControlTower controlTower = new ControlTower();

      AirForce airForce = new AirForce();

      while (!isDone)
      {
        foreach (AirCraft craft in airForce.Crafts)
        {
          controlTower.ReqeustForLanding(craft);
          Thread.Sleep(2000); //Each Request comes after 1 sec
        }
        Console.WriteLine("Control tower going for maintenance for 10 sec");
        Thread.Sleep(10000);
      }
      Console.WriteLine("Control tower exiting...");
    }
  }

  /// <summary>
  /// Control Tower is acting as a mediator for all the crafts to help them doing landing
  /// </summary>
  public class ControlTower
  {
    public Queue<AirCraft> craftsQueue = new Queue<AirCraft>();
    public Object padLock = new Object();


    public ControlTower()
    {
      Thread th = new Thread(Run);  // start the tower
      th.Start();
    }

    public void ReqeustForLanding(AirCraft craft)
    {
      lock (padLock)
      {
        craftsQueue.Enqueue(craft);
        Monitor.PulseAll(padLock); //notify waiting thread 
      }
    }

    public void Run()
    {
      while (true)
      {
        Monitor.Enter(padLock);
        while (craftsQueue.Count == 0)
        {
          Console.WriteLine("No more flights..");
          Monitor.Wait(padLock);
          Console.WriteLine("Flight reqeust came...");
        }

        AirCraft craft = craftsQueue.Dequeue();
        Console.WriteLine($"Permission given to {craft.ToString()} to perform landing");
        //we have only one runway so only single one to land
        craft.Land();
        Thread.Sleep(3000); // letting craft land...

        Monitor.Exit(padLock);
      }
    }

  }
}
