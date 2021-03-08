using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{
  public class Semaphore_Usages
  {

    public static void StartMethod()
    {

      //CantReleaseMoreThanMaxLimit();

      new SemaphorePingPongExample().runTest();
      //new SingleSemaphorePingPongExample().runTest();
      
      Console.WriteLine("Semaphore usage ended. Press any key to continue");
      Console.ReadLine();
    }

    //SemaphoreFullException is thrown if Release() is invoked on a semaphore object that already has reached its maximum count.
    public static void CantReleaseMoreThanMaxLimit()
    {
      Semaphore sem = new Semaphore(0, 1);
      sem.Release();
      sem.Release();
    }

    //Code shows how semaphore can be used as mutex to provide synchronization across critical section
    public void LockIngWithSemaphore()
    {
      //declare below line as member variable of the usage class.
      //inital count of 1 means one thread can proceed without waiting.
      //if initial count were set to 0, then no thread would able to proceed until some thread call the release, which would increase the count to 1 and then waiting thread would acquire the lock and move forward.
      Semaphore sem = new Semaphore(1, 1);


      sem.WaitOne();
      // critical section
      sem.Release();
    }
  }

  //Ping pong using Semephores.
  public class SemaphorePingPongExample
  {
    // can we initialize both sem as (1,1)? No. Refer notes for explaination. In short calling Release before calling wait will cause an exception of max count
    Semaphore sem1 = new Semaphore(0, 1);    
    Semaphore sem2 = new Semaphore(0, 1);
    volatile bool isDone = false;
    void pong()
    {
      while (!isDone)
      {
        sem2.Release();
        sem1.WaitOne();
        Console.WriteLine("Pong");
        Thread.Sleep(1000);
      }
    }

    void ping()
    {
      while (!isDone)
      {
        sem2.WaitOne();
        Console.WriteLine("Ping");
        sem1.Release();
        Thread.Sleep(1000);
      }
    }

    public void runTest()
    {
      Thread pingThread = new Thread(() =>
      {
        ping();
      });


      Thread pongThread = new Thread(() =>
      {
        pong();
      });

      pongThread.Start();
      pingThread.Start();

      Thread.Sleep(10000); //let the child threads run for 10 sec
      isDone = true;

      pongThread.Join();
      pingThread.Join();
    }
  }

  //if someone thinks that this ping pong problem can be solved with single semaphore, thats not correct.
  //Consider the following sequence:
  //  pingThread prints ping
  //  pingThread releases the semaphore
  //  At this point either pingThread can continue execution and re-acquire the just released semaphore or pongThread can be woken up to resume execution. If pingThread is chosen to continue then ping will be printed in succession.And if pongThread is chosen then we see pong get printed which is the desired outcome.
  //In fact, this question can't be solved correctly with a single semaphore.
  public class SingleSemaphorePingPongExample
  {
    Semaphore sem = new Semaphore(0, 1);

    void pong()
    {
      while (true)
      {
        sem.WaitOne();
        Console.WriteLine("Pong");
        sem.Release();
      }
    }

    void ping()
    {
      while (true)
      {
        Console.WriteLine("Ping");
        sem.Release();
        sem.WaitOne();
      }
    }

    public void runTest()
    {
      Thread pingThread = new Thread(() =>
      {
        ping();
      });


      Thread pongThread = new Thread(() =>
      {
        pong();
      });

      pongThread.Start();
      pingThread.Start();

      pongThread.Join();
      pingThread.Join();
    }
    //In fact, this question can't be solved correctly with a single semaphore.
  }

}
