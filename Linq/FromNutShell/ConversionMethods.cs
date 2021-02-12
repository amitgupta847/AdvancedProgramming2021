using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Linq.Example2;

namespace Linq.FromNutShell
{
  public class ConversionMethods
  {
    public static void StartMethod()
    {
      // casting();
      //  OfTypeAndCastUsage();

      ToDict_Usage();
    }

    public static void casting()
    {
      int i = 10;
      long iLong = (long)i;

      Object iObj = i;
      long i2Long = (long)iObj;
    }

    //OfType and Cast
    //From non generic to generic - or useful in downcastig elements in a generic input
    public static void OfTypeAndCastUsage()
    {
      ArrayList classicList = new ArrayList();
      classicList.AddRange(new int[] { 3, 4, 5 });

      DateTime offender = DateTime.Now;
      classicList.Add(offender);

      Console.WriteLine("Using OfType");
      IEnumerable<int> newSeq = classicList.OfType<int>();

      foreach (int element in newSeq)
      {
        Console.Write(element + " ");   // ofType ignore the element if it is not of expected type.
      }

      Console.WriteLine("\nUsing Cast");
      IEnumerable<int> newSeq2 = classicList.Cast<int>();
      try
      {
        foreach (int element in newSeq2)
        {
          Console.Write(element + " ");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("\nCast throws an exception if element is not of expected type");
      }


    }

    //ToDictionary
    public static void ToDict_Usage()
    {
      // new Movie { Title="The Dark Knight", Rating=8.9f, Year=2008},
      List<Movie> movies = Movie.getMovies();

      Dictionary<String, Movie> dict = movies.ToDictionary(mov => mov.Title);

      foreach (var entry in dict)
      {
        Console.WriteLine($"{ entry.Key} : {entry.Value}");
      }

      //Lets add a duplicate entry in the list:
      Console.WriteLine("\n\nLets add the duplicate element in the list to see the behavior of ToDict:\n");
      movies.Add(new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008 });

      try
      {
        dict = movies.ToDictionary(mov => mov.Title);
        foreach (var entry in dict)
        {
          Console.WriteLine($"{ entry.Key} : {entry.Value}");
        }
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine(ex.Message);
        Console.WriteLine("For the given Key, Each element must evaluate to unique value");

      }

      

      // new Movie { Title="The Dark Knight", Rating=8.9f, Year=2008},
    }

  }
}
