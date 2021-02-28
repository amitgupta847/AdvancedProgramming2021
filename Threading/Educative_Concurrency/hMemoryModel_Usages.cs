using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{

  // Note:We don't need to insert memory barriers when using synchronization primitives as they implicitly generate memory fences. 

  public class MemoryModel_Usages
  {
    public static void StartMethod()
    {
      new PossibleReorderingExample().runTest();
    }
  }

  public class PossibleReorderingExample
  {
    bool keepGoing = true;

    public void work()
    {
      while (keepGoing)
      {
        Console.WriteLine("Doing something important");
        Thread.MemoryBarrier();
      }
    }


    public void runTest()
    {
      Thread childThread = new Thread(() =>
      {
        work();
      });
      childThread.Start();


      // let child thread run for a second
      Thread.Sleep(1000);

      // update the flag
      keepGoing = false;
      Thread.MemoryBarrier();

      // wait for child thread to terminate
      childThread.Join();

    }
  }


}//namespace


