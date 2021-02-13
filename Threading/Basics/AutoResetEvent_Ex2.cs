using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Basics
{
  public class AutoResetEvent_Ex2
  {
    static EventWaitHandle _ready = new AutoResetEvent(false);
    static EventWaitHandle _go = new AutoResetEvent(false);
    static bool _isDone = false;

    static readonly object _locker = new object();

    //Two way signalling
    public static void StartMethod()
    {
      Console.WriteLine("Main thread Starting");
      Console.WriteLine("Hey New thread, let me know when you are ready. I will pass on to you to do your work");

      //start the new thread
      Thread newThread = new Thread(Waiter);
      newThread.Name = "New Thread";
      newThread.Start();

      Console.WriteLine("Main thread waiting for ready signal");
      _ready.WaitOne();  //main thread waiting for new thread to signal when it is ready to perform work
      Console.WriteLine("Main thread giving the go signal");
      _go.Set();

      Thread.Sleep(10);
      Console.WriteLine("\nMain thread waiting for next ready signal");
      _ready.WaitOne();
      Console.WriteLine("Main thread giving the go signal");
      _go.Set();

      Thread.Sleep(10);
      Console.WriteLine("\nMain thread waiting for next ready signal");
      _ready.WaitOne();

      lock (_locker) _isDone = true;

      Console.WriteLine("Main thread giving the go signal");
      _go.Set();


      newThread.Join();
      Console.WriteLine("Main thread done");
    }

    public static void Waiter()
    {
      while (true)
      {
        Console.WriteLine($"{Thread.CurrentThread.Name} - Telling am ready");
        _ready.Set(); //signalling that i am ready
        Console.WriteLine($"{Thread.CurrentThread.Name} - Waiting for go signal");
        _go.WaitOne(); //here i will wait for go signal
        Console.WriteLine("THanks for allowing me to work....Now i am doing work");
        Thread.Sleep(5000);

        Console.WriteLine($"\n{Thread.CurrentThread.Name} - My work is done");

        lock (_locker)
        {
          if (_isDone)
          {
            Console.WriteLine($"\n{Thread.CurrentThread.Name} - Finally i am returning...");
            break;
          }
        }

      }
    }
  }

}

