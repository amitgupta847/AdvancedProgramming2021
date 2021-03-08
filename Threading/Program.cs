using System;
using Threading.Basics;
using Threading.C1_MultiThreading;
using Threading.C2_TPL_FinStockData;
using Threading.Educative_Concurrency;
using Threading.Educative_Concurrency.InterviewProblems;

namespace Threading
{
  class Program
  {
    static void Main(string[] args)
    {

      //ThreadsBasics.StartMethod();
      //AutoResetEvent_Ex1.StartMethod();
      //AutoResetEvent_Ex2.StartMethod();


      //Educative
       aEducative.StartMethod();
       // InterviewProblemsStarter.StartMethod();


      //PlayWithFinStockData(args);


      // Task_Ex1.StartMethod();
      //AsyncAwait.StartMethod();

      Console.WriteLine("Press any key to quit!");
      Console.ReadLine();
    }


    private static void PlayWithFinStockData(string[] args)
    {
      //SimulateStockData_Seq.StartMethod(args);
      // SimulateStockData_AsyncOldWay.StartMethod(args);

      SimulateStockProcessing_TPL.StartMethod(args);
    }
  }
}
