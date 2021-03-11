using OOPs_And_DesignPatterns.cBehavioral.CommandPattern.AirCraftExample;
using OOPs_And_DesignPatterns.cBehavioral.MediatorPattern.AirCraftExample;
using OOPs_And_DesignPatterns.cBehavioral.StatePattern.AirCraftExample;
using OOPs_And_DesignPatterns.cBehavioral.VisitorPattern.AirCraftExample;
using OOPs_And_DesignPatterns.cBehavioral.VisitorPattern.AirCraftExample2;
using OOPs_And_DesignPatterns.cBehavioral.VisitorPattern.AirCraftExample3;
using System;

namespace OOPs_And_DesignPatterns
{
  class Program
  {
    static void Main(string[] args)
    {



      //Behavioral
      //AirCraft_V1_AsCommandPattern.StartMethod()
      //AirCraft_V1_AsMediatorPattern.StartMethod();
      aAirCraft_StatePattern.StartMethod();

      //AirCraft_V1_AsVisitorPattern.StartMethod();
      //AirCraft_V2_AsVisitorPattern.StartMethod();
      //AirCraft_V3_DoubleDispatcher.StartMethod();

      Console.WriteLine("\nDesign pattern testing done!");
      Console.ReadLine();
    }
  }
}
