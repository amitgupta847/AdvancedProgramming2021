using Linq.Example1;
using Linq.Example2;
using System;
using System.Linq;
using System.Collections.Generic;
using Linq.Example3;

namespace Linq
{
  class Program
  {
    static void Main(string[] args)
    {
      //Example 1
      //filesAndLINQ();

      //Example2
      //MoviesAndLINQ();

      //Learning streaming and its usage
      // print10RandomNumbers();

      ProcessCars();
      Console.WriteLine("Press any key to continue");
      Console.ReadLine();
    }


    private static void filesAndLINQ()
    {
      Files.ShowLargeFilesWithoutLINQ(@"C:\Windows");
      Files.ShowLargeFilesWithLINQ(@"C:\Windows");
    }

    private static void MoviesAndLINQ()
    {
      Movie.Movies();
      //Movie.MoviesInOrder();
      //Movie.customFilter();
    }

    private static void print10RandomNumbers()
    {
      var query = MyLinq.Random().Where(num => num > 0.5).Take(10);
      foreach (double number in query)
      {
        Console.WriteLine($"{number,5:N2}");
      }
    }

    private static void ProcessCars()
    {
      //CarQueries.PrintCarsRating();
      //CarQueries.PrintCarsRatingQuerySyntax();

      CarQueries.PrintCarsStatistics();
    }

  }
}
