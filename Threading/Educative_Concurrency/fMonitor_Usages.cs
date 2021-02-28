using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency.Monitor1
{
  public class Monitor_Usages
  {

    public static void StartMethod()
    {
      //new PrimeFinderMonitor().run();

      // new PrimeFinderMonitor_Multiple().run();

      // new PingPong().Play_PingPong();

      // new QuizQuestion1().run();

      //new QuizQuestion2().run();

      new QuizQuestion4().run();
    }

  }


  public class PrimeFinderMonitor
  {
    bool found = false;
    int prime;
    volatile bool shutdown = false;
    Object lockObj = new Object();

    private bool isPrime(int i)
    {
      if (i == 2 || i == 3) return true;

      int div = 2;

      while (div <= i / 2)
      {
        if (i % div == 0) return false;
        div++;
      }

      return true;
    }


    public void printer()
    {
      while (!shutdown)
      {
        Monitor.Enter(lockObj);

        while (!found)
        {
          Monitor.Wait(lockObj);
        }

        Console.WriteLine("Prime found to be = " + prime);

        found = false;
        Monitor.Pulse(lockObj);
        Monitor.Exit(lockObj);

      }
    }

    public void finder()
    {
      prime = 2;
      while (!shutdown)
      {

        if (isPrime(prime))
        {
          Monitor.Enter(lockObj);

          found = true;
          Monitor.Pulse(lockObj);

          while (found)
          {
            Monitor.Wait(lockObj);
          }

          Monitor.Exit(lockObj);
        }

        prime++;

      }
    }


    public void run()
    {

      Thread primeFinderThread = new Thread(new ThreadStart(finder));
      Thread printerThread = new Thread(new ThreadStart(printer));

      primeFinderThread.Start();
      printerThread.Start();

      Thread.Sleep(1000);

      shutdown = true;

      printerThread.Join();
      primeFinderThread.Join();

    }
  }


  //2nd one

  //Prime finder with multiple finder thread and usage of PulseAll and finally
  //moving prime++ in a critical section.
  public class PrimeFinderMonitor_Multiple
  {
    bool found = false;
    int prime;
    volatile bool shutdown = false;
    Object lockObj = new Object();

    private bool isPrime(int i)
    {
      if (i == 2 || i == 3) return true;

      int div = 2;

      while (div <= i / 2)
      {
        if (i % div == 0) return false;
        div++;
      }

      return true;
    }


    public void printer()
    {
      while (!shutdown)
      {
        Monitor.Enter(lockObj);

        while (!found && !shutdown)
        {
          Monitor.Wait(lockObj);
        }

        Console.WriteLine("Prime found to be = " + prime);

        found = false;
        Monitor.PulseAll(lockObj);
        Monitor.Exit(lockObj);
      }

      // Wake up any blocked finder threads
      // Monitor.Enter(lockObj);
      // Monitor.PulseAll(lockObj);
      // Monitor.Exit(lockObj);
    }


    //We'd want to alternate the runs of finder and printer threads and in order to block a consecutive run of finder threads we move the Monitor.Wait() call wrapped in the while loop at the top in the finder thread code.
    public void finder()
    {
      while (!shutdown)
      {
        Monitor.Enter(lockObj);

        while (found && !shutdown)
        {
          Monitor.Wait(lockObj);
        }

        if (isPrime(prime))
        {
          found = true;
          Monitor.PulseAll(lockObj);
        }

        prime++;
        Monitor.Exit(lockObj);
      }

      // Wake up any finder threads
      // Monitor.Enter(lockObj);
      // Monitor.PulseAll(lockObj);
      // Monitor.Exit(lockObj);
    }


    public void run()
    {

      Thread primeFinderThread = new Thread(new ThreadStart(finder));
      Thread primeFinderThread2 = new Thread(new ThreadStart(finder));
      Thread primeFinderThread3 = new Thread(new ThreadStart(finder));
      Thread printerThread = new Thread(new ThreadStart(printer));

      primeFinderThread.Start();
      primeFinderThread2.Start();
      primeFinderThread3.Start();
      printerThread.Start();


      Thread.Sleep(1000);

      shutdown = true;

      printerThread.Join();
      primeFinderThread2.Join();
      primeFinderThread3.Join();
      primeFinderThread.Join();
    }
  }


  //Ping Pong Using Monitors
  public class PingPong
  {
    bool isPing = true;

    Object _sync = new object();

    //multithread display of ping pong l0 times
    public void Play_PingPong()
    {
      Thread ping = new Thread(() => { Ping(); });
      Thread pong = new Thread(() => { Pong(); });

      // ping.IsBackground = false;     // incase you dont use Join, then your main thread will exit without printing output for these threads, because bydefault these threads are background
      // pong.IsBackground = false;

      ping.Start();
      pong.Start();

      ping.Join();
      pong.Join();
    }

    int numberOfTimes = 10;

    public void Ping()
    {
      int i = 0;
      while (i < numberOfTimes)
      {
        Monitor.Enter(_sync);
        while (!isPing)
        {
          Monitor.Wait(_sync);
        }

        Console.WriteLine("Ping");
        isPing = false;

        Monitor.Pulse(_sync);
        Monitor.Exit(_sync);

        Thread.Sleep(1000);
        i++;
      }
    }

    public void Pong()
    {
      int i = 0;
      while (i < numberOfTimes)
      {
        Monitor.Enter(_sync);
        while (isPing)
        {
          Monitor.Wait(_sync);
        }

        Console.WriteLine("Pong");
        isPing = true;

        Monitor.Pulse(_sync);
        Monitor.Exit(_sync);

        Thread.Sleep(1000);
        i++;
      }
    }

  }


  //This question if run on a Mac will throw an exception but if run in the code-widget would deadlock as Monitor.Exit() is invoked by a different thread than the one that invoked Monitor.Enter().
  public class QuizQuestion1
  {

    private readonly Object obj = new Object();


    void MonitorExit()
    {
      Thread.Sleep(500);
      Monitor.Exit(obj);
    }


    void MonitorEnter()
    {
      Monitor.Enter(obj);
      Thread.Sleep(500);
    }

    public void run()
    {
      Thread t1 = new Thread(new ThreadStart(MonitorEnter));
      Thread t2 = new Thread(new ThreadStart(MonitorExit));

      t1.Start();
      t2.Start();

      t1.Join();
      t2.Join();

      Console.WriteLine("Hello");
      Monitor.Enter(obj);
      Console.WriteLine("World");
      Monitor.Exit(obj);
    }
  }


  //Depends, thread 2 can be blocked, or it can print and move forward based on scheduling.

  //Amit - in my case, it was always blocked. Deadlocked. because object was not exited second time.
  public class QuizQuestion2
  {
    private readonly Object obj = new Object();

    public void enterTwice()
    {

      Monitor.Enter(obj);
      Monitor.Enter(obj);
      Console.WriteLine("Hello");
      Monitor.Exit(obj); 
    }

    public void enterOnce()
    {
      Monitor.Enter(obj);
      Console.WriteLine("World");
      Monitor.Exit(obj);
    }

    public void run()
    {
      Thread thread1 = new Thread(new ThreadStart(enterTwice));
      thread1.Start();
      thread1.Join();

      Thread thread2 = new Thread(new ThreadStart(enterOnce));
      thread2.Start();
      thread2.Join();

    }
  }

  //This question is using value type, which further get boxed to an object, for syncronization.

  public class QuizQuestion4
  {
    private Object obj = false;

    public void printMessage()
    {

      Monitor.Enter(obj);
      obj = true;
      Thread.Sleep(3000);
      Monitor.Exit(obj);
      Console.WriteLine("All is good");
    }

    public void run()
    {

      Thread t1 = new Thread(new ThreadStart(printMessage));
      t1.Start();

      Thread t2 = new Thread(new ThreadStart(printMessage));
      t2.Start();

      t1.Join();
      t2.Join();
    }
  }
}
