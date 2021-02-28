using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency.Lock1
{
  public class Lock_Usages
  {
    public static void StartMethod()
    {
      new PingPong().Play_PingPong();

      // new QuizQuestion1().run();

      //new QuizQuestion2().run();

      //new QuizQuestion4().run();
    }

  }

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
        lock (_sync)
        {
          while (!isPing)
          {
            Monitor.Wait(_sync);
          }

          Console.WriteLine("Ping");
          isPing = false;

          Monitor.Pulse(_sync);
        }

        Thread.Sleep(1000);
        i++;
      }
    }

    public void Pong()
    {
      int i = 0;
      while (i < numberOfTimes)
      {
        lock (_sync)
        {
          while (isPing)
          {
            Monitor.Wait(_sync);
          }

          Console.WriteLine("Pong");
          isPing = true;

          Monitor.Pulse(_sync);
        }
        Thread.Sleep(1000);
        i++;
      }
    }

  }

}//namespace


