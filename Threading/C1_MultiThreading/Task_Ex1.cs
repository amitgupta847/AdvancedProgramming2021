using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.C1_MultiThreading
{
  public class Task_Ex1
  {
    public static void StartMethod()
    {
      // TaskUsage();
      // TaskContinuationUsage();
      //TaskWaitingUsage();

      TestTaskWrite();
    }


    //Showing usage of Task and the way to create a task
    private static void TaskUsage()
    {
      var cts = new CancellationTokenSource();


      //Task.Factory.StartNew(() => Print(), CancellationToken.None, TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning, TaskScheduler.Default);

      //or
      // Task.Factory.StartNew(() => Print());

      //or
      Task<int> t1 = Task.Run(() => Print(cts.Token), cts.Token);

      Thread.Sleep(15);
      cts.Cancel();
      //or
      // Task<int> t2 = Task.Run(()=> Print(cts.Token), cts.Token);
      try
      {
        Console.WriteLine($"The first task processed: {t1.Result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      // Console.WriteLine($"The second task processed: {t2.Result}");
      Console.Read();
    }

    private static int Print(CancellationToken cancelToken)
    {
      Console.WriteLine($"Is Threadpool thread: {Thread.CurrentThread.IsThreadPoolThread}");
      int sum = 0;
      Thread.Sleep(5000);
      for (int i = 0; i < 50; i++)
      {
        if (cancelToken.IsCancellationRequested)
        {
          Console.WriteLine("Cancellation Requested");
          //break;   // break is not a good idea.. as state of task will be changed to completed in stead of canceled.
          // so use next shown statement
        }
        cancelToken.ThrowIfCancellationRequested();


        sum = sum + i;
        Console.Write($"{i} MID={Thread.CurrentThread.ManagedThreadId} ");
      }
      return sum;
    }

    //Task Continution
    private static void TaskContinuationUsage()
    {
      var cts = new CancellationTokenSource();
      Task<int> t1 = Task.Run(() => Print(cts.Token), cts.Token);


      //once t1 finishes then t2 will start. i.e. its in serial...
      var t2 = t1.ContinueWith(prevTask =>
        {
          Console.WriteLine($"\nWhat is the result from first task? {t1.Result} ");
          var t2 = Task.Run(() => Print(cts.Token), cts.Token);
        });

      t1.ContinueWith((prev) =>
      {
        //we can start a next task based on the condition like if prev task faulted.
        Console.WriteLine("finally we are here");

      }, TaskContinuationOptions.OnlyOnFaulted);
      //conditional contiuation

      Console.WriteLine("Main thread is not block");
      Console.Read();
    }

    //Waiting for a task.
    //it will block the caller thread
    //Task Continution
    private static void TaskWaitingUsage()
    {
      var cts = new CancellationTokenSource();
      Task<int> t1 = Task.Run(() => Print(cts.Token), cts.Token);
      var t2 = Task.Run(() => Print(cts.Token), cts.Token);

      Console.WriteLine("Started t1 & t2");
      //Task.WaitAll(t1, t2);
      Console.WriteLine("Finished t1 & t2");


      Console.WriteLine("Main thread is finishing");
      Console.Read();
    }



    //Learning I/O based tasks
    static string _filePath = @"C:\demo.txt";
    private static void TestTaskWrite()
    {
      FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 8, true);

      string content = "A quick brown fox jumps over the lazy dog";
      byte[] data = Encoding.Unicode.GetBytes(content);

      Task task = fs.WriteAsync(data, 0, data.Length);
      task.ContinueWith((t) =>
      {
        fs.Close();
        TestAsyncTaskRead();
      });
    }

    private static void TestAsyncTaskRead()
    {
      FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None, 8, true);
      byte[] data = new byte[1024];
      Task<int> readTask = fs.ReadAsync(data, 0, data.Length);
      readTask.ContinueWith((t) =>
      {
        fs.Close();
        string content = Encoding.Unicode.GetString(data, 0, t.Result);
        Console.WriteLine($"Read Completed. Content is: {content}");
      });


    }
  }


  class WebClientWrapper
  {

    private WebClient wc = new WebClient();

    //The thing is that registering for a cancellation event is way better than polling the token by calling its CancellationRequest flag.
    private async Task LongRunningOperation(CancellationToken t)
    {
      if (!t.IsCancellationRequested)
      {
        using (CancellationTokenRegistration ctr = t.Register(() => { wc.CancelAsync(); })) ;
        {
          wc.DownloadStringAsync(new Uri("http://www.engineerspock.com"));
        }
      }
    }


  }
}
