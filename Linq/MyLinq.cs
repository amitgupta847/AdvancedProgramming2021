using Linq.Example3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
  public static class MyLinq
  {

    //Learning: Streaming opeator understanding
    public static IEnumerable<double> Random()
    {
      var random = new Random();
      while (true)
      {
        yield return random.NextDouble();
      }
    }

    //Learning: understanding how LINQ works using yield
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
      foreach (var item in source)
      {
        if (predicate(item))
          yield return item;
      }
    }



    //Learning: understanding how LINQ works, but using yield
    //public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    //{
    //  var result = new List<T>();

    //  foreach (var item in source)
    //  {
    //    if (predicate(item))
    //      result.Add(item);
    //  }

    //  return result;
    //}



    public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
    {
      foreach (var line in source)
      {
        yield return Car.Parse(line);
      }
    }


  }
}
