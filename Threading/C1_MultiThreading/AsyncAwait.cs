using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.C1_MultiThreading
{
  public class AsyncAwait
  {
    public static void StartMethod()
    {
      // Catcher();
      MultipleExceptionEx();
      Console.Read();
    }

    async static Task Catcher()
    {
      try
      {
        Task thrower = Thrower();
        await thrower;
      }
      catch (InvalidOperationException ex)
      { }
    }

    private static async Task Thrower()
    {
      await Task.Delay(100);
      throw new InvalidOperationException();
    }


    //Showing usage of multiple tasks throwing exceptions.
    private static async void MultipleExceptionEx()
    {
      int[] numbers = { 0 };
      Task<int> t1 = Task.Run(() => 5 / numbers[0]); //1st exception
      Task<int> t2 = Task.Run(() => numbers[1]);    //2nd Exception

      Task<int[]> allTasks = Task.WhenAll(t1, t2);  
      //You can not use Task.WaitAll(t1, t2), since you are using 
      //async await pattern;

      try
      {
        await allTasks;
      }
      catch
      //(Exception ex)  // if you attempt to catch general exception, 
      //you will see only the first one here, i.e Divide by zero. 
      {
        foreach (var ex1 in allTasks.Exception.InnerExceptions)
        {
          Console.WriteLine(ex1);
        }
      }
    }
  }
}
