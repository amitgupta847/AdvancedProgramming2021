using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Threading.C1_MultiThreading
{
  public class Task_Ex1
  {
    public static void StartMethod()
    {
      // TaskUsage();
      // TaskContinuationUsage();
      // TaskWaitingUsage();

      // TestTaskWrite();
      // TestException();
      // TestAggregateException();
      NestedTasks();
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


    //Learning Exception Handling
    public static void TestException()
    {
      var t1 = Task.Run(() => method());
      try
      {
        //best advice to handle exceptions for the tasks where main thread do not wait is to configure continuation and observe IsFaulted or use the TaskContinuationOptions as Only faulted
        //Note: here below t1 is waiting, still we configured the Continuation for knowledge purpose
        t1.ContinueWith((t1) =>
       {
         if (t1.IsFaulted)
           Console.WriteLine("I am faulted");
         else if (t1.IsCompleted)
           Console.WriteLine("I am completed succesfully");
       });

        t1.Wait();  // if there was any exception in the method that task performed, that will be wrapped in Aggregate exception and will be throw at this point

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.GetType().ToString());
        Console.WriteLine(ex.Message);

        //Here instead of general Exception, you could configure to recieve only AggregateException and flatten it to see all the exceptions in it.
      }
    }

    public static void method()
    {
      throw new ArgumentException();
    }

    //Example 2: Lets create a task which further creates 3 tasks and all those throws an exception.
    private static void TestAggregateException()
    {
      var parent = Task.Factory.StartNew(() =>
      {
        //We will throw 3 exceptions at once using 3 child tasks:
        int[] numbers = { 0 };
        var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

        childFactory.StartNew(() => 5 / numbers[0]); // divide by zero
        childFactory.StartNew(() => numbers[1]); // index out of range
        childFactory.StartNew(() => throw null); //null reference
      });

      try
      {
        parent.Wait();
      }
      catch (AggregateException aex)
      {
        //here we are showing the usage of handle by handling 2 of the exception and finally null reference exception will be thrown;
        aex.Flatten().Handle(ex =>
        {
          if (ex is DivideByZeroException)
          {
            Console.WriteLine("Divide by zero");
            return true;
          }
          if (ex is IndexOutOfRangeException)
          {
            Console.WriteLine("Index out range");
            return true;
          }

          return false;
        });
      }

    }



    //Nested and child tasks
    private static void NestedTasks()
    {
      Task.Factory.StartNew(() =>
      {
        Task nested = Task.Factory.StartNew(() => Console.WriteLine("hello i am child task"));
      }).Wait();


      // parent.Wait();
    }

    //a way to parallelize the work
    //Below example shows how the main task is getting the files from directory and then passing that file to each new child task in order to process each one individually, rather sequentially.
    public Task ImportXMLFileAsync(string dataDirectory, CancellationToken cts)
    {
      Task t = Task.Factory.StartNew(() =>
      {
        foreach (FileInfo file in new DirectoryInfo(dataDirectory).GetFiles("*.xml"))
        {
          string fileToProcess = file.FullName;
          Task.Factory.StartNew(() =>
          {

            cts.ThrowIfCancellationRequested();
            XElement doc = XElement.Load(fileToProcess);
            //InternalProcessXml(doc, cts);
          }, TaskCreationOptions.AttachedToParent);
        }

      }, cts);

      
      return t;
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
