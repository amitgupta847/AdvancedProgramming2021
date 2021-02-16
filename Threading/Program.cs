using System;
using Threading.Basics;
using Threading.C1_MultiThreading;

namespace Threading
{
  class Program
  {
    static void Main(string[] args)
    {

      //ThreadsBasics.StartMethod();
      //AutoResetEvent_Ex1.StartMethod();
      //AutoResetEvent_Ex2.StartMethod();

      Task_Ex1.StartMethod();
      //AsyncAwait.StartMethod();

      Console.WriteLine("Press any key to quit!");
      Console.ReadLine();
    }


    
  }
}
