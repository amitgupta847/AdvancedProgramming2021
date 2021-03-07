using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency.InterviewProblems
{
  public class P4_ImplementingSemaphore
  {
    private CountSemaphore sem = new CountSemaphore(1);
    public void task2()
    {
      Thread.Sleep(2000);
      Console.WriteLine("releasing");
      sem.Release();

      Thread.Sleep(2000);
      Console.WriteLine("releasing");
      sem.Release();

      Thread.Sleep(2000);
      Console.WriteLine("releasing");
      sem.Release();
    }

    public void task1()
    {
      // consume the first parameter
      sem.Acquire();

      Console.WriteLine("acquiring");
      sem.Acquire();

      Console.WriteLine("acquiring");
      sem.Acquire();

      Console.WriteLine("acquiring");
      sem.Acquire();

    }

    public void run()
    {
      Thread t1 = new Thread(new ThreadStart(task2));
      Thread t2 = new Thread(new ThreadStart(task1));

      t2.Start();
      Thread.Sleep(10000);
      t1.Start();


      t1.Join();
      t2.Join();
    }
  }

  //Custom Implementation for semahores using monitors
  public class CountSemaphore
  {
    private readonly int MAX_PERMITS;
    private int givenOut;
    private object padlock = new object();

    public CountSemaphore(int maxPermits)
    {
      this.MAX_PERMITS = maxPermits;
      givenOut = 0;
    }

    public void Acquire()
    {
      Monitor.Enter(padlock);

      while (givenOut == MAX_PERMITS)
      {
        Monitor.Wait(padlock);
      }

      givenOut += 1;
      Monitor.PulseAll(padlock);
      Monitor.Exit(padlock);
    }

    public void Release()
    {
      Monitor.Enter(padlock);

      while (givenOut == 0)
      {
        Monitor.Wait(padlock);
      }

      givenOut--;
      Monitor.PulseAll(padlock);
      Monitor.Exit(padlock);
    }


  }
}
