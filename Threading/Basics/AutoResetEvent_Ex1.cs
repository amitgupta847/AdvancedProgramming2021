using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Basics
{
  public class AutoResetEvent_Ex1
  {
    static EventWaitHandle _waitHandle = new AutoResetEvent(false);

    //a thread is started whose job is simply to wait until signaled by another thread
    public static void StartMethod()
    {
      Console.WriteLine("Main thread Starting");
      Thread newThread = new Thread(Waiter);
      newThread.Name = "New Thread";
      newThread.Start();
   
      Thread.Sleep(5000);
     
      _waitHandle.Set();
  
      Console.WriteLine("Main thread done");
    }

    public static void Waiter()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} - Waiting....");
        _waitHandle.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.Name} - Notified..");
      
    }

  }
}
