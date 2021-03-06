using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency.InterviewProblems
{
  //A blocking queue is defined as a queue which blocks the caller of the enqueue method if there's no more capacity to add the new item being enqueued. Similarly, the queue blocks the dequeue caller if there are no items in the queue. Also, the queue notifies a blocked enqueuing thread when space becomes available and a blocked dequeuing thread when an item becomes available in the queue.

  public class P1_BoundedBuffer
  {
    //private BlockingQueue_MutexBased bq = new BlockingQueue_MutexBased(1);
    private BlockingQueue_MonitorBased bq = new BlockingQueue_MonitorBased(1);

    int i = 1;
    int num_Steps = 100;

    private void producerThread()
    {
      int data = 1;
      while (i <= num_Steps)
      {
        bq.enqueue(data);
        Console.WriteLine("Thread with id " + Thread.CurrentThread.ManagedThreadId + " produced = " + data);
        data++;

        Interlocked.Increment(ref i);
        Thread.Sleep(100);
      }
    }

    private void consumerThread()
    {
      while (true)
      {
        int data = bq.dequeue();
        Console.WriteLine("Thread with id " + Thread.CurrentThread.ManagedThreadId + " consumed = " + data);
        Thread.Sleep(100);
      }
    }

    public void run()
    {
      ThreadStart producerThreadStart = new ThreadStart(this.producerThread);
      Thread producer = new Thread(producerThreadStart);
      producer.Start();

      Thread producer2 = new Thread(producerThreadStart);
      producer2.Start();

      Thread producer3 = new Thread(producerThreadStart);
      producer3.Start();

      ThreadStart consumerThreadStart = new ThreadStart(this.consumerThread);
      Thread consumer = new Thread(consumerThreadStart);
      consumer.Start();

      Thread consumer2 = new Thread(consumerThreadStart);
      consumer2.Start();

      Thread consumer3 = new Thread(consumerThreadStart);
      consumer3.Start();

      // Run simulation for 1 second
      //Thread.Sleep(1000);

    }
  }


  //Let's see how the implementation would look like, if we were restricted to using a Mutex object to solve the problem. A Mutex only provides mutual exclusion and doesn't facilitate threads to wait and be signaled. Without the ability to wait or signal the implication is, a blocked thread will constantly poll in a loop for a predicate/condition to become true before making progress. This is an example of a busy-wait solution.
  public class BlockingQueue_MutexBased
  {
    private int[] items;
    private int maxSize;

    private int currentSize;
    private int tail;
    private int head;

    private Mutex _mutex = new Mutex();

    public BlockingQueue_MutexBased(int size)
    {
      this.maxSize = size;
      this.currentSize = 0;
      this.items = new int[size];
      this.tail = 0;
      this.head = 0;
    }

    public void enqueue(int data)
    {
      _mutex.WaitOne();

      while (currentSize == maxSize)
      {
        // Release the mutex to give other threads
        // a chance to acquire it
        _mutex.ReleaseMutex();

        // Reacquire the mutex before checking the
        // condition

        _mutex.WaitOne();
      }

      if (tail == maxSize)
        tail = 0;

      items[tail] = data;
      currentSize++;
      tail++;

      _mutex.ReleaseMutex();
    }

    public int dequeue()
    {
      _mutex.WaitOne();

      int result = -1;
      while (currentSize == 0)
      {
        _mutex.ReleaseMutex();

        _mutex.WaitOne();
      }

      if (head == maxSize)
        head = 0;

      currentSize--;
      result = items[head];
      head++;
      _mutex.ReleaseMutex();

      return result;
    }
  }


  //We can also implement the bounded buffer using the Monitor class. In fact, using the Monitor class saves us from busy-waiting, as a blocked thread can relinquish the monitor until it gets signaled.
  public class BlockingQueue_MonitorBased
  {
    private int[] items;
    private int maxSize;

    private int currentSize;
    private int tail;
    private int head;

    //private Mutex _mutex = new Mutex();

    public BlockingQueue_MonitorBased(int size)
    {
      this.maxSize = size;
      this.currentSize = 0;
      this.items = new int[size];
      this.tail = 0;
      this.head = 0;
    }

    public void enqueue(int data)
    {
      Monitor.Enter(items);

      while (currentSize == maxSize)
      {
        Monitor.Wait(items);
      }

      if (tail == maxSize)
        tail = 0;

      items[tail] = data;
      currentSize++;
      tail++;

      Monitor.PulseAll(items);
      Monitor.Exit(items);
    }

    public int dequeue()
    {
      int result;
      Monitor.Enter(items);

      while (currentSize == 0)
      {
        Monitor.Wait(items);
      }

      if (head == maxSize)
        head = 0;

      currentSize--;
      result = items[head];
      head++;

      Monitor.PulseAll(items);
      Monitor.Exit(items);

      return result;
    }
  }

}


