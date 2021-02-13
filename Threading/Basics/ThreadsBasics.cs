using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Basics
{
  public class ThreadsBasics
  {
    public static void StartMethod()
    {

      try
      {
        Thread t1 = new Thread(() => WriteY("Y"));
        t1.Start();
      }
      catch (Exception ex)
      {
        //Console.WriteLine(ex.Message);
      }

      for (int i = 0; i < 1000; i++)
        Console.Write("X");

      //t1.Join();
      Console.WriteLine("\nMain Thread Done");
    }



    public static void WriteY(string xORy )
    {
      //throw new ArgumentException();
      Thread.Sleep(2000);
      for (int i = 0; i < 1000; i++)
        Console.Write(xORy);

    }
  }
}
