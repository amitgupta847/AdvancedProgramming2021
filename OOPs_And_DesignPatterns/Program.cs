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
      //AirCraft_V1_AsVisitorPattern.StartMethod();
      //AirCraft_V2_AsVisitorPattern.StartMethod();
      AirCraft_V3_DoubleDispatcher.StartMethod();

      Console.WriteLine("Design pattern testing done!");
      Console.ReadLine();
    }
  }
}
