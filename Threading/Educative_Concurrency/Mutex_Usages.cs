using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{
  public class Mutex_Usages
  {

    public static void StartMethod()
    {
      Mutex_Usages obj = new Mutex_Usages();
      
      obj.Q1();
    }


    //Aborted Mutex Exceptio will be thrown because Mutex is not released as many times as it is acquired
    public void Q1()
    {
      Mutex mutex = new Mutex();

      Thread t1 = new Thread(() =>
      {
        // Child thread locks the mutex
        // twice but releases it only once
        mutex.WaitOne();
        mutex.WaitOne();
        mutex.ReleaseMutex();
       });

      t1.Start();
      t1.Join();

      // Main thread attemps to acquire the mutex
      mutex.WaitOne();
      Console.WriteLine("All Good");
      mutex.ReleaseMutex();
    }
  }
}
