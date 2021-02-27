using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency
{
  public class Interlocked_Usages
  {
    public static void StartMethod()
    {
      //new InterlockedAddExample().runTest();

      //new PingPong().Play();
      new PingPong().Play_MultiThreaded();
    }

  }





  //Ping Pong Using Interlocked
  public class PingPong
  {
    bool isPing = true;
    //single threaded display of ping pong 10 times.
    public void Play()
    {
      for (int i = 1; i <= 10; i++)
      {
        if (isPing)
          Console.WriteLine("Ping");
        else
          Console.WriteLine("Pong");

        isPing = !isPing;
      }
    }

    long flag = 0;

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
        while (Interlocked.Read(ref flag) == 1)  // keep in lool if value is 1
        {
        }
        Console.WriteLine("Ping");
        Interlocked.Exchange(ref flag, 1);
        Thread.Sleep(1000);
        i++;
      }

    }


    public void Pong()
    {
      int i = 0;
      while (i < numberOfTimes)
      {
        while (Interlocked.Read(ref flag) == 0)  // keep in lool if value is 0
        {
        }
        Console.WriteLine("Pong");
        Interlocked.Exchange(ref flag, 0);
        Thread.Sleep(1000);
        i++;
      }
    }

  }



  //Consider the example below.We create 10 threads and each threads increments the 
  //instance variable j 100000 times.
  //At the end, if correctly programmed j should equal 100000. 
  //You'll see different values for the summation each time you run the code widget.
  //(if we dont synchronize the shared variable j, below we used interlocked to synchronize the accees and hence every thread corrently added 1 to the value and at last it is 1000000)

  public class InterlockedAddExample
  {
    long j = 0;

    public void runTest()
    {

      Thread[] threads = new Thread[10];

      // Create 10 threads
      for (int i = 0; i < 10; i++)
      {
        threads[i] = new Thread(() =>
        {
          for (long k = 0; k < 100000; k++)
          {
            //j++;
            Interlocked.Add(ref j, 1);
          }
        });
      }

      // Run all 10 threads
      for (int i = 0; i < 10; i++)
      {
        threads[i].Start();
      }

      // Wait for all the threads to finish
      for (int i = 0; i < 10; i++)
      {
        threads[i].Join();
      }

      Console.WriteLine("Value of j = " + j);
    }
  }
}
