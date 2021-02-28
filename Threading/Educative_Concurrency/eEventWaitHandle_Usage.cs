using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{
  public class eEventWaitHandle_Usage
  {
    public static void StartMethod()
    {
      Console.WriteLine("\nLets see the behavior with Manual and Auto reset event both first.");
      new EventWaitHandleExample().runTest();

      Console.WriteLine("\nNow see the behavior with both as Auto reset event");
      new AutoResetEventExample().runTest();
    }

  }

  // In the example below, the main thread spawns two child threads that block on a EventWaitHandle object ewh.
  // The main thread sets the ewh object after sleeping for a second and at the same time waits on another EventWaitHandle object done. This is possible using the static method WaitHandle.SignalAndWait() of the EventWaitHandle class. 
  //the done object is set by both the spawned threads and the main thread is able to exit.
  public class EventWaitHandleExample
  {
    EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
    EventWaitHandle done = new EventWaitHandle(false, EventResetMode.AutoReset);

    void work(int num)
    {
      ewh.WaitOne();
      Console.WriteLine(num * num);

      done.Set();
    }

    public void runTest()
    {
      Thread t1 = new Thread(() =>
      {
        work(5);
      });

      Thread t2 = new Thread(() =>
      {
        work(10);
      });

      t1.Start();
      t2.Start();

      Thread.Sleep(1000);

      WaitHandle.SignalAndWait(ewh, done);  //you can use this SignalAnWait method instead below shown lines:
      //ewh.Set();
      //done.WaitOne();

      Console.WriteLine("Main thread exiting");

      t1.Join();
      t2.Join();
    }
  }



  //In contrast to EventWaitHandle, the AutoResetEvent allows a single thread to be released when signaled. 
  //We rewrite the same example using AutoResetEvent objects below. 
  //Note that only a single blocked thread is released when the AutoResetEvent object is signaled. Additionally, the object doesn't remain in the signaled state and resets itself back to unsignaled.
  public class AutoResetEventExample
  {
    AutoResetEvent arEvent = new AutoResetEvent(false);
    AutoResetEvent done = new AutoResetEvent(false);

    void work(int num)
    {
      arEvent.WaitOne();
      Console.WriteLine(num * num);

      done.Set();
    }
   
    public void runTest()
    {

      Thread t1 = new Thread(() => {
        work(5);
      });
      t1.IsBackground = true;

      Thread t2 = new Thread(() => {
        work(10);
      });
      t2.IsBackground = true;

      t1.Start();
      t2.Start();

      Thread.Sleep(1000);

      arEvent.Set();
      done.WaitOne();

      Console.WriteLine("Main thread exiting");

    }
  }
}
