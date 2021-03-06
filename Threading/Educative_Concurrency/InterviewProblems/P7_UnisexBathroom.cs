using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.Educative_Concurrency.InterviewProblems
{
  public class P7_UnisexBathroom
  {
    public void run()
    {

      //Let's first write the code so that only one person is allowed in the bathroom at one time.(either male or female)
      // UnisexBathRoom_OnlyOnePersonAtOneTime bathroom = new UnisexBathRoom_OnlyOnePersonAtOneTime();

      //Now Let's change code to have any number of people but either male or female
      //UnisexBathRoom_AnyNumberButMaleOrFemaleOnly bathroom = new UnisexBathRoom_AnyNumberButMaleOrFemaleOnly();


      //Final solution - Max 3 people in bathroom, all should be men or all should be women at one point in time
      UnisexBathRoom bathroom = new UnisexBathRoom();

      Thread female1 = new Thread(new ParameterizedThreadStart(bathroom.UseByFemale));
      Thread male1 = new Thread(new ParameterizedThreadStart(bathroom.UseByMale));
      Thread male2 = new Thread(new ParameterizedThreadStart(bathroom.UseByMale));
      Thread female2 = new Thread(new ParameterizedThreadStart(bathroom.UseByFemale));
      Thread male3 = new Thread(new ParameterizedThreadStart(bathroom.UseByMale));
      Thread male4 = new Thread(new ParameterizedThreadStart(bathroom.UseByMale));
      Thread male5 = new Thread(new ParameterizedThreadStart(bathroom.UseByMale));
      Thread male6 = new Thread(new ParameterizedThreadStart(bathroom.UseByMale));

      female1.Start("Lisa");
      male1.Start("John");
      male2.Start("Bob");
     // Thread.Sleep(1000);
      female2.Start("Natasha");
      male3.Start("Anil");
      male4.Start("Wentao");
      male5.Start("Nihkil");
      male6.Start("Paul");

      female1.Join();
      female2.Join();
      male1.Join();
      male2.Join();
      male3.Join();
      male4.Join();
      male5.Join();
      male6.Join();

      Console.WriteLine(String.Format("Employees in bathroom at the end {0}", bathroom.getEmpsInBathroom()));

    }
  }


  public enum UseBy
  {
    Male,
    Female,
    None
  }

  // A bathroom is being designed for the use of both males and females in an office but requires the following constraints to be maintained:
  //•	There cannot be men and women in the bathroom at the same time.
  //•	There should never be more than three employees in the bathroom simultaneously.
  //The solution should avoid deadlocks.For now, though, don’t worry about starvation.



  //Amit: Lets solve above mentioned problem one by one.
  //Let's first write the code so that only one person is allowed in the bathroom at one time.(either male or female)
  public class UnisexBathRoom_OnlyOnePersonAtOneTime
  {
    public object padLock = new object();
    UseBy inUseBy = UseBy.None;
    int countInBathRoom = 0;
    public void UseByMale(Object name)
    {
      Monitor.Enter(padLock);      //putting lock at whole block will let only one person use the bathroom at one time
      while (inUseBy == UseBy.Female)
      {
        Monitor.Wait(padLock);
      }
      inUseBy = UseBy.Male;
      countInBathRoom++;
      UseBathroom(name);
      
      inUseBy = UseBy.None;
      countInBathRoom--;
      Monitor.PulseAll(padLock);
      Monitor.Exit(padLock);
    }

    public void UseByFemale(Object name)
    {
      Monitor.Enter(padLock);
      while (inUseBy == UseBy.Male)
      {
        Monitor.Wait(padLock);
      }
      inUseBy = UseBy.Female;
      countInBathRoom++;
      UseBathroom(name);
      inUseBy = UseBy.None;
      countInBathRoom--;
      Monitor.PulseAll(padLock);
      Monitor.Exit(padLock);
    }

    private void UseBathroom(Object name)
    {
      Console.WriteLine(String.Format("\n{0} is using the bathroom. {1} employees in bathroom", name, countInBathRoom));
      Thread.Sleep(3000);
      Console.WriteLine(String.Format("\n{0} is done using the bathroom", name));
    }

    public int getEmpsInBathroom()
    {
      return countInBathRoom;
    }
  }


  //Amit: so far above code allows one person in bathroom.
  //now lets change code to have any number of people but either male or female
  public class UnisexBathRoom_AnyNumberButMaleOrFemaleOnly
  {
    public object padLock = new object();
    UseBy inUseBy = UseBy.None;
    int countInBathRoom = 0;
    public void UseByMale(Object name)
    {
      Monitor.Enter(padLock);
      while (inUseBy == UseBy.Female)
      {
        Monitor.Wait(padLock);
      }
      inUseBy = UseBy.Male;
      countInBathRoom++;
      
      //just before a person goes in bathroom, he should leave the look so that any other person should grab the lock come inside the bathroom
      Monitor.Exit(padLock);
            
      UseBathroom(name);
      
      Monitor.Enter(padLock);
      
      countInBathRoom--;
      Console.WriteLine(String.Format("\n{0} Decrement the count. Count is {1}", name, countInBathRoom));
      if (countInBathRoom==0)
        inUseBy = UseBy.None;
         
      Monitor.PulseAll(padLock);
      Monitor.Exit(padLock);

    }

    public void UseByFemale(Object name)
    {
      Monitor.Enter(padLock);
      while (inUseBy == UseBy.Male)
      {
        Monitor.Wait(padLock);
      }
      inUseBy = UseBy.Female;
      countInBathRoom++;

      //just before a person goes in bathroom, he should leave the look so that any other person should grab the lock come inside the bathroom
      Monitor.Exit(padLock);

      UseBathroom(name);

      Monitor.Enter(padLock);
      countInBathRoom--;
      Console.WriteLine(String.Format("\n{0} Decrement the count. Count is {1}", name, countInBathRoom));
      if (countInBathRoom == 0)
        inUseBy = UseBy.None;
      
      Monitor.PulseAll(padLock);
      Monitor.Exit(padLock);
    }

    private void UseBathroom(Object name)
    {
      Console.WriteLine(String.Format("\n{0} is using the bathroom at {1}. {2} employees in bathroom", name, DateTime.Now.ToLongTimeString(), countInBathRoom));
      Thread.Sleep(5000);
      Console.WriteLine(String.Format("\n{0} is done using the bathroom", name));
    }

    public int getEmpsInBathroom()
    {
      return countInBathRoom;
    }
  }


  // A bathroom is being designed for the use of both males and females in an office but requires the following constraints to be maintained:
  //•	There cannot be men and women in the bathroom at the same time.
  //•	There should never be more than three employees in the bathroom simultaneously.
  //The solution should avoid deadlocks.For now, though, don’t worry about starvation.
  public class UnisexBathRoom
  {
    public object padLock = new object();
    UseBy inUseBy = UseBy.None;
    int countInBathRoom = 0;
    Semaphore semMaxInBath = new Semaphore(3, 3);

    public void UseByMale(Object name)
    {
      Monitor.Enter(padLock);
      while (inUseBy == UseBy.Female)
      {
        Monitor.Wait(padLock);
      }

      //amit- incase you are thnking to exit the monitor here, beform waiting on semaphore, 
      //so the lock can be given to thread coming from bathroom inorder to decrement the count, then you are going on wrong direction.
      //you can not exit the monitor before setting its condition, here it is inuseb. else male and female both thread potentially can reach here and would go to bathroom together

      semMaxInBath.WaitOne(); // once we know who can go inside, we need to limit the count
      countInBathRoom++;
      Console.WriteLine(String.Format("\n{0} INcrement the count. Count is {1}", name, countInBathRoom));
      inUseBy = UseBy.Male;

      Monitor.Exit(padLock); //we must release the lock on padlock to let other thread enter the bathroom. If we dont release the lock, other threads will be blocked and only one thread will able to use bathroom at one time, whereas we want 3 people to use at any time

      UseBathroom(name);
      semMaxInBath.Release();

      Monitor.Enter(padLock);
      countInBathRoom--;
      Console.WriteLine(String.Format("\n{0} Decrement the count. Count is {1}", name, countInBathRoom));
      if (countInBathRoom == 0)
        inUseBy = UseBy.None;

      Monitor.PulseAll(padLock);
      Monitor.Exit(padLock);
    }

    public void UseByFemale(Object name)
    {
      Monitor.Enter(padLock);
      while (inUseBy == UseBy.Male)
      {
        Monitor.Wait(padLock);
      }

      semMaxInBath.WaitOne(); // once we know who can go inside, we need to limit the count
      countInBathRoom++;
      Console.WriteLine(String.Format("\n{0} INcrement the count. Count is {1}", name, countInBathRoom));
      inUseBy = UseBy.Female;

      Monitor.Exit(padLock); //we must release the lock on padlock to let other thread enter the bathroom. If we dont release the lock, other threads will be blocked and only one thread will able to use bathroom at one time, whereas we want 3 people to use at any time

      UseBathroom(name);
      semMaxInBath.Release();

      Monitor.Enter(padLock);
      countInBathRoom--;
      Console.WriteLine(String.Format("\n{0} Decrement the count. Count is {1}", name, countInBathRoom));
      if (countInBathRoom == 0)
        inUseBy = UseBy.None;

      Monitor.PulseAll(padLock);
      Monitor.Exit(padLock);
    }

    private void UseBathroom(Object name)
    {
      Console.WriteLine(String.Format("\n{0} is using the bathroom at {1}. {2} employees in bathroom", name, DateTime.Now.ToLongTimeString(), countInBathRoom));
      Thread.Sleep(5000);
      Console.WriteLine(String.Format("\n{0} is done using the bathroom", name));
    }

    public int getEmpsInBathroom()
    {
      return countInBathRoom;
    }

    //If you look at the program output, you'd notice that the number of current employees in the bathroom is printed out to be greater than 3 at times even though the maximum allowed employees in the bathroom are 3. This is just an outcome of how the code is structured, read notes for an explanation.
  }
}
