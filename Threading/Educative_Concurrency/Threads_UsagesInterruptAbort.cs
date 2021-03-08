using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{
  public class Threads_UsagesInterruptAbort
  {
    public static void StartMethod()
    {
      // new ThreadInterruptExample().runTest();
      new ThreadAbortExample().runTest();
    }
  }

  public class ThreadInterruptExample
  {

    void childThread()
    {

      try
      {
        Thread.Sleep(Timeout.Infinite);
      }
      catch (ThreadInterruptedException)
      {
        Console.WriteLine("caught exception");
      }
      finally
      {
        // empty block
      }
      Console.WriteLine("Child thread exiting");

    }


    public void runTest()
    {

      Thread child = new Thread(() =>
      {
        childThread();
      });

      child.Start();

      // wait for child thread to block on Sleep

      Thread.Sleep(1000);

      // now interrupt the child thread
      child.Interrupt();

      // wait for child thread to finish
      child.Join();

      Console.WriteLine("Main thread exiting");
    }
  }

  public class ThreadAbortExample
  {

    void childThread()
    {

      try
      {
        Thread.Sleep(Timeout.Infinite);
      }
      catch (ThreadAbortException)
      {
        Console.WriteLine("caught exception");
        Thread.ResetAbort();
      }
      finally
      {
        // empty block
      }

      Console.WriteLine("Child thread exiting");

    }

    //We rewrite the above example but instead of interrupting the child thread, we abort it.
    public void runTest()
    {
      Thread child = new Thread(() =>
      {
        childThread();
      });

      child.Start();

      // wait for child thread to block on Sleep

      Thread.Sleep(1000);

      // Now interrupt the child thread
      child.Abort();

      child.Join();

      Console.WriteLine("Main thread exiting");

    }
  }
}
