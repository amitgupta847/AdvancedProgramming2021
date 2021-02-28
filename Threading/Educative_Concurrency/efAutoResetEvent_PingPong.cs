using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{
  public class AutoResetEvent_PingPong
  {
    public static void StartMethod()
    {
      new PingPong_Auto().Play_MultiThreaded();
    }

  }

  //Ping Pong Using Interlocked
  public class PingPong_Auto
  {
    AutoResetEvent pingEv = new AutoResetEvent(false);
    AutoResetEvent pongEv = new AutoResetEvent(false);

    //multithread display of ping pong l0 times
    public void Play_MultiThreaded()
    {
      Thread ping = new Thread(() => { Ping(); });
      Thread pong = new Thread(() => { Pong(); });

      // ping.IsBackground = false;
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
        pongEv.WaitOne();
        Console.WriteLine("Ping");
        pingEv.Set();
        Thread.Sleep(1000);
        i++;
      }
    }

    public void Pong()
    {
      int i = 0;
      while (i < numberOfTimes)
      {
        pongEv.Set();
        pingEv.WaitOne();
        Console.WriteLine("Pong");
        Thread.Sleep(1000);
        i++;
      }
    }

  }

}
